using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform canvas;
    [SerializeField]
    Transform RevivePos;
    [SerializeField]
    GameObject targetUI;

    [SerializeField]
    Stat stat;

    public MyStat myStat;

    [SerializeField]
    Joystick joy;

    Rigidbody rigid;

    //public Monster targetMonster;

    Dictionary<Define.EquipmentType, Item> equipment = new Dictionary<Define.EquipmentType, Item>();

    public Inventory inven;

    CharacterController characterController;
    [SerializeField]
    public Transform textTransform;

    [SerializeField]
    bool lockOn = false;
    bool alive = true;
    public bool isGround = true;
    public bool canMove = true;
    public bool defence = false;
    bool atkCheck = false;

    public int dotDmg;
    public float dotTime;

    float gravity = 9.81f;

    Animator anim;

    Collider wallFinder;

    [SerializeField]
    Define.PlayerState state = Define.PlayerState.Idle;
    public Define.DOTType dotType = Define.DOTType.NONE;

    public Monster attackTarget;
    public NPC nearNPC;

    public List<Quest> myQuest = new List<Quest>();

    public bool LockOn { get { return lockOn; } set { lockOn = value; } }
    public Animator Anim { get { return anim; } }
    public Define.PlayerState State { get { return state; } set { state = value; } }
    public Dictionary<Define.EquipmentType, Item> Equipment { get { return equipment; } set { equipment = value; } }
    public Dictionary<int, bool> questClear = new Dictionary<int, bool>();
    public Dictionary<Skill, int> mySkill = new Dictionary<Skill, int>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        Managers.Sound.BGMPlay("Dungeon");
        rigid = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        myStat = new MyStat(stat);
        questClear.Add(0, true);
    }

    private void Update()
    {
        if (!alive)
            return;

        if(myStat.currHP <= 0) { Die(); return; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject go = UIButton.uiQueue.Dequeue();
            go.SetActive(false);
        }

        anim.SetBool("Defence", defence);

        switch (state)
        {
            case Define.PlayerState.Idle:
                Idle();
                break;
            case Define.PlayerState.Moving:
                Move();
                break;
            case Define.PlayerState.Attack:
                Attack();
                break;
        }
    }

    public void Revive()
    {
        if(SceneManager.GetActiveScene().name != "Game")
        {
            LoadSceneManager.Loading("Game");
            Revive();
        }

        alive = true;
        anim.SetTrigger("Revive");
        RevivePos = GameObject.Find("RevivePos").transform;

        transform.position = RevivePos.position;
        myStat.currHP = myStat.HP;
    }

    void Die()
    {
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();

        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].State = Define.MonsterState.Idle;
            monsters[i].AttackTarget = null;
        }

        alive = false;
        anim.SetTrigger("Die");

        Invoke("DieTooltip", 0.5f);
    }

    void DieTooltip()
    {
        Managers.Tooltip.ToolTipCreate(canvas, Define.TooltipType.DIE);
    }

    void Idle()
    {
        anim.SetBool("Defence", false);
        anim.SetBool("Move", false);
    }

    void Move()
    {
        //if (!canMove)
        //{
        //    return;
        //}
        //transform.position += Joystick.moveVec;
        if(characterController.collisionFlags != CollisionFlags.Below)
            Joystick.moveVec.y -= gravity * Time.deltaTime;

        characterController.Move(Joystick.moveVec);

        Debug.Log(Joystick.moveVec);

        anim.SetBool("Move", true);
    }

    void AttackFunc()
    {
        if (attackTarget == null)
        {
            Managers.Sound.SFXPlay("Player_Attack_NonTarget");
            return;
        }

        int rand = UnityEngine.Random.Range(0, 100);

        if (rand > 80)
            Managers.Sound.SFXPlay($"{attackTarget.myStat.Name}_Hit");

        attackTarget.Hit(this, dotType);
        targetUI.SetActive(true);
    }

    void Attack()
    {
        if (!lockOn)
        {
            attackTarget = null;
            GetAttackTarget();
        }

        if (atkCheck)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                canMove = true;
                atkCheck = false;
                state = Define.PlayerState.Idle;
                return;
            }
        }
        else
        {
            atkCheck = true;
            canMove = false;
            if (attackTarget != null)
                transform.LookAt(attackTarget.transform.position);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                anim.SetTrigger("Attack");
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            {
                anim.SetTrigger("Attack");
                anim.SetBool("Move", false);
                joy.Drag = false;
                Joystick.moveVec = Vector3.zero;
                joy.RectJoy.localPosition = Vector3.zero;
            }
        }
    }

    public void AttackBtn()
    {
        state = Define.PlayerState.Attack;
    }

    void GetAttackTarget()
    {
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();

        for (int i = 0; i < monsters.Length; i++)
        {
            if (Vector3.Distance(monsters[i].transform.position, transform.position) <= stat.atkDistance)
            {
                if (attackTarget == null)
                    attackTarget = monsters[i];
                else
                {
                    if (attackTarget.myStat.HP < monsters[i].myStat.HP)
                        attackTarget = monsters[i];
                }
            }
        }
    }

    public void Hit(Monster monster)
    {
        int dmg = monster.myStat.AD - myStat.DEF;

        if (dmg <= 0)
            dmg = 1;

        myStat.currHP -= dmg;
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            anim.SetTrigger("Hit");
        FloatingText.DamageText(transform, dmg.ToString(), Color.red);
        BOSSROOM.Hit();
    }

    public void Hit(Monster monster, Define.DOTType type)
    {
        Hit(monster);

        if (type == Define.DOTType.NONE)
            return;
        DotDamage dot;

        dot = GetComponent<DotDamage>();
        if (dot == null)
            dot = gameObject.AddComponent<DotDamage>();

        dot.Init(monster.dotDmg, monster.dotTime, type);
    }

    public void LevelUp()
    {
        attackTarget = null;
        if (myStat.currEXP >= myStat.EXP)
        {
            myStat.Level += 1;
            myStat.currEXP -= myStat.EXP;
            myStat.statPoint += Managers.Data.EXPData(myStat.Level - 1).Item2;
            myStat.skillPoint += 1;
            myStat.EXP = Managers.Data.EXPData(myStat.Level).Item1;
            GameObject effect = Instantiate(Resources.Load("Particle/LevelUP") as GameObject, transform);
            Managers.Sound.SFXPlay("LevelUP");
            Destroy(effect, 3f);
        }
    }

    public void TalkNPC()
    {
        nearNPC.SendMessage("Talk");
    }

    public Tuple<List<string>, List<string>> mySkillReturns()
    {
        List<string> skills = new List<string>();
        List<string> levels = new List<string>();

        for (int i = 0; i < Managers.Data.skills.Count; i++)
        {
            if (mySkill.ContainsKey(Managers.Data.skills[i]))
            {
                skills.Add(Managers.Data.skills[i].Index.ToString());
                levels.Add(mySkill[Managers.Data.skills[i]].ToString());
            }
        }
        Tuple<List<string>, List<string>> data = new Tuple<List<string>, List<string>>(skills, levels);

        return data;
    }

    public void SkillEffect(Skill skill)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger($"BuffSkill");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            anim.SetTrigger("BuffSkill");
            anim.SetBool("Move", false);
            joy.Drag = false;
            Joystick.moveVec = Vector3.zero;
            joy.RectJoy.localPosition = Vector3.zero;
        }
    }
}
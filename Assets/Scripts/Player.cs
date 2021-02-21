using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    Transform textTransform;

    bool alive = true;
    public bool isGround = true;
    public bool canMove = true;
    bool atkCheck = false;

    float gravity = 9.81f;

    Animator anim;

    Collider wallFinder;

    [SerializeField]
    Define.PlayerState state = Define.PlayerState.Idle;

    Monster attackTarget;
    public NPC nearNPC;

    public List<Quest> myQuest = new List<Quest>();

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject go = UIButton.uiQueue.Dequeue();
            go.SetActive(false);
        }

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
            case Define.PlayerState.Die:
                Die();
                break;
        }
    }

    void Idle()
    {
        anim.SetBool("Move", false);
    }

    void Move()
    {
        //if (!canMove)
        //{
        //    return;
        //}
        //transform.position += Joystick.moveVec;
        Joystick.moveVec.y -= gravity * Time.deltaTime;
        characterController.Move(Joystick.moveVec);
        anim.SetBool("Move", true);
    }

    void Die()
    {
        alive = false;
    }

    void AttackFunc()
    {
        if (attackTarget == null)
        {
            Managers.Sound.SFXPlay("Player_Attack_NonTarget");
            return;
        }
        Managers.Sound.SFXPlay($"{attackTarget.myStat.Name}_Hit");

        attackTarget.SendMessage("Hit", this);
    }

    void Attack()
    {
        attackTarget = null;
        GetAttackTarget();

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
            if(attackTarget != null)
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
                Debug.Log(Vector3.Distance(monsters[i].transform.position, transform.position));
                Debug.Log(Vector3.Distance(transform.position, monsters[i].transform.position));

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
        anim.SetTrigger("Hit");
        FloatingText.DamageText(textTransform, dmg.ToString(), Color.red);
    }

    public void LevelUp()
    {
        if(myStat.currEXP >= myStat.EXP)
        {
            myStat.Level += 1;
            myStat.currEXP -= myStat.EXP;
            myStat.statPoint += Managers.Data.EXPData(myStat.Level - 1).Item2;
            myStat.skillPoint += 1;
            myStat.EXP = Managers.Data.EXPData(myStat.Level).Item1;
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
            anim.SetTrigger("BuffSkill");
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    float currMP, MP;
    public float dotTime;
    public int dotDmg;
    protected bool channeling = false;

    [SerializeField]
    Stat stat;
    protected float gravity = 9.81f;
    public MyStat myStat;

    protected Animator anim;

    protected Vector3 patrolVec;

    public MonsterSpawner spawner;

    protected CharacterController controller;
    [SerializeField]
    public Transform textTransform;

    [SerializeField]
    protected Define.MonsterState state = Define.MonsterState.Idle;

    public Define.MonsterType type = Define.MonsterType.None;

    protected Player attackTarget;

    protected bool isAlive = true;

    protected bool canMove = false, canAttack = false;

    protected float elapsedTime = 0f;

    bool dieTrigger = false;

    public Define.MonsterState State { get { return state; } set { state = value; } }
    public Player AttackTarget { set { attackTarget = value; } }


    private void OnEnable()
    {
        myStat = new MyStat(stat);
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DieCheck();
        if (!isAlive)
        {
            Die();
            state = Define.MonsterState.Die;
            return;
        }

        if (!controller.isGrounded)
        {
            patrolVec.y -= gravity * Time.deltaTime;
        }

        if (!canAttack)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > myStat.atkTime)
            {
                elapsedTime = 0f;
                canAttack = true;
            }
        }

        if(type == Define.MonsterType.Drake && state != Define.MonsterState.Idle && !channeling)
        {
            currMP += Time.deltaTime;

            if (currMP > MP)
            {
                currMP = 0;
                state = Define.MonsterState.SKILL;
            }
        }

        switch (state)
        {
            case Define.MonsterState.Idle:
                Idle();
                break;
            case Define.MonsterState.Patrol:
                Patrol();
                break;
            case Define.MonsterState.Moving:
                Moving();
                break;
            case Define.MonsterState.Trace:
                Trace();
                break;
            case Define.MonsterState.Attack:
                Attack();
                break;
            case Define.MonsterState.TRAP:
                Trap();
                break;
            case Define.MonsterState.SKILL:
                Skill();
                break;
        }
    }

    protected virtual void Skill() { }

    protected virtual void Idle() { }
    protected virtual void Moving() { }
    protected virtual void Attack() { }
    protected virtual void Patrol() { }
    protected virtual void Trace() { }
    protected virtual void Hit(Player player)
    {
        int dmg = player.myStat.AD - myStat.DEF;

        myStat.currHP -= dmg;
        anim.SetTrigger("Hit");
        FloatingText.DamageText(textTransform, dmg.ToString(), Color.white);

        attackTarget = player;
        if (state != Define.MonsterState.Attack)
            state = Define.MonsterState.Trace;

        DieCheck();
        if (!isAlive)
        {
            player.myStat.currEXP += myStat.currEXP;
        }
    }

    public virtual void Hit(Player player, Define.DOTType type)
    {
        Hit(player);

        if (type == Define.DOTType.NONE)
            return;
        DotDamage dot;

        dot = GetComponent<DotDamage>();
        if (dot == null)
            dot = gameObject.AddComponent<DotDamage>();

        dot.Init(player.dotDmg, player.dotTime, type);
    }

    public virtual void Trap()
    {
        if (attackTarget == null)
            attackTarget = GameObject.FindObjectOfType<Player>();
        GameObject effect = Instantiate(Resources.Load("Particle/MonsterSpawn") as GameObject, transform);
        state = Define.MonsterState.Trace;
    }

    protected void DieCheck()
    {
        if (myStat.currHP <= 0)
        {
            isAlive = false;
            return;
        }
        isAlive = true;
    }

    void Die()
    {
        if (!dieTrigger)
        {
            dieTrigger = true;
            anim.SetTrigger("Die");
        }
        else
        {
            myStat.dieElapsed += Time.deltaTime;

            if (myStat.dieElapsed > myStat.dieanimationTime)
            {
                MonsterPool.DieMonster(this);
                attackTarget.LevelUp();
            }
        }
    }
}

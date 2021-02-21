using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    Stat stat;
    protected float gravity = 9.81f;
    public MyStat myStat;

    protected Animator anim;

    protected Vector3 patrolVec;

    public MonsterSpawner spawner;

    protected CharacterController controller;
    [SerializeField]
    protected Transform textTransform;

    [SerializeField]
    protected Define.MonsterState state = Define.MonsterState.Idle;

    public Define.MonsterType type = Define.MonsterType.None;

    protected Player attackTarget;

    protected bool isAlive = true;

    protected bool canMove = false, canAttack = false;

    bool dieTrigger = false;


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

        if(!controller.isGrounded)
        {
            patrolVec.y -= gravity * Time.deltaTime;
        }

        switch (state)
        {
            case Define.MonsterState.Idle:
                Idle();
                break;
            case Define.MonsterState.Patrol:
                Patrol();
                break;
            case Define.MonsterState.Trace:
                Trace();
                break;
            case Define.MonsterState.Attack:
                Attack();
                break;
        }
    }

    protected virtual void Idle() { }
    protected virtual void Moving() { }
    protected virtual void Attack() { }
    protected virtual void Patrol() { }
    protected virtual void Trace() { }
    protected virtual void Hit(Player player) { }

    protected void DieCheck()
    {
        if(myStat.currHP <= 0)
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

            if(myStat.dieElapsed > myStat.dieanimationTime)
            {
                MonsterPool.DieMonster(this);
                attackTarget.LevelUp();
            }
        }
    }
}

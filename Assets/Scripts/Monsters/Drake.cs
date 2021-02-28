using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drake : Monster
{
    private void Start()
    {
        type = Define.MonsterType.Drake;
    }

    protected override void Hit(Player player)
    {
        attackTarget = player;
    }

    protected override void Idle()
    {
        anim.SetBool("Move", false);

        if (attackTarget != null)
            state = Define.MonsterState.Trace;
    }

    protected override void Trace()
    {
        float distance = Vector3.Distance(transform.position, attackTarget.transform.position);

        if(distance > myStat.atkDistance)
        {
            state = Define.MonsterState.Moving;
        }
    }

    protected override void Moving()
    {
        float distance = Vector3.Distance(transform.position, attackTarget.transform.position);

        anim.SetBool("Move", true);

        Vector3 moveVec = attackTarget.transform.position - transform.position;
        moveVec.Normalize();

        transform.LookAt(attackTarget.transform);

        controller.Move(moveVec * myStat.moveSpeed * Time.deltaTime);

        if (distance < myStat.atkDistance)
        {
            state = Define.MonsterState.Attack;
        }
    }

    protected override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            anim.SetBool("Move", false);
            anim.SetTrigger("Attack");
            state = Define.MonsterState.Trace;
        }
    }
}
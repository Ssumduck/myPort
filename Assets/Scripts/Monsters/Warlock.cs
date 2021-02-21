﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Monster
{
    float elapsedTime = 0f;
    
    protected override void Idle()
    {
        anim.SetBool("Move", false);

        elapsedTime += Time.deltaTime;

        if(elapsedTime > myStat.moveTime)
        {
            elapsedTime = 0f;

            patrolVec = RandomUtil.RandomVector3(spawner.transform, spawner.radius);
            state = Define.MonsterState.Patrol;
        }
    }

    protected override void Patrol()
    {
        elapsedTime += Time.deltaTime;

        transform.LookAt(new Vector3(patrolVec.x, transform.position.y, patrolVec.z));

        Vector3 vec;

        vec = patrolVec - transform.position;
        vec = vec.normalized;

        float distance = Vector3.Distance(patrolVec, transform.position);


        if(distance <= 0.2f || elapsedTime > 4f)
        {
            elapsedTime = 0f;
            state = Define.MonsterState.Idle;
            return;
        }

        anim.SetBool("Move", true);
        controller.Move(vec * myStat.moveSpeed * Time.deltaTime);
    }

    protected override void Hit(Player player)
    {
        int dmg = player.myStat.AD - myStat.DEF;

        myStat.currHP -= dmg;
        anim.SetTrigger("Hit");
        FloatingText.DamageText(textTransform, dmg.ToString(), Color.white);

        attackTarget = player;
        if(state != Define.MonsterState.Attack)
            state = Define.MonsterState.Trace;

        DieCheck();
        if(!isAlive)
        {
            player.myStat.currEXP += myStat.currEXP;
        }
    }

    protected override void Trace()
    {
        if (attackTarget == null)
        {
            state = Define.MonsterState.Idle;
            return;
        }

        if (!canMove)
        {
            anim.SetBool("Move", false);
            myStat.moveElapsed += Time.deltaTime;
        }

        if (myStat.moveElapsed > myStat.moveTime)
        {
            myStat.moveElapsed = 0f;

            canMove = true;
        }

        if (canMove)
        {
            if (myStat.atkDistance > Vector3.Distance(transform.position, attackTarget.transform.position))
            {
                Debug.Log("ATTACK");
                canMove = false;
                state = Define.MonsterState.Attack;
            }
            else
            {
                myStat.moveElapsed += Time.deltaTime;

                if(myStat.moveElapsed > myStat.moveDuration)
                {
                    myStat.moveElapsed = 0f;
                    canMove = false;
                    return;
                }

                Vector3 vec = (attackTarget.transform.position - transform.position).normalized;
                anim.SetBool("Move", true);
                transform.LookAt(attackTarget.transform);
                controller.Move(vec * myStat.moveSpeed * Time.deltaTime);
            }
        }
    }

    protected override void Attack()
    {
        if (attackTarget == null)
            state = Define.MonsterState.Idle;
        else if (Vector3.Distance(attackTarget.transform.position, transform.position) > myStat.atkDistance)
            state = Define.MonsterState.Trace;

        myStat.moveElapsed += Time.deltaTime;

        if (myStat.moveElapsed > myStat.atkTime)
        {
            canAttack = true;
            myStat.moveElapsed = 0f;
        }

            if (canAttack)
        {
            canAttack = false;
            anim.SetBool("Attack", true);
            attackTarget.Hit(this);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            patrolVec = transform.position;
        }
    }
}
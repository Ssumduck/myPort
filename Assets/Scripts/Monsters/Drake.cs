using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drake : Monster
{
    [SerializeField]
    AnimationClip clip;
    [SerializeField]
    Transform skill_effect;



    private void Start()
    {
        type = Define.MonsterType.Drake;
    }

    protected override void Idle()
    {
        anim.SetBool("Move", false);
    }

    protected override void Trace()
    {
        float distance = Vector3.Distance(transform.position, attackTarget.transform.position);

        if(distance > myStat.atkDistance)
        {
            state = Define.MonsterState.Moving;
        }
        else
        {
            state = Define.MonsterState.Attack;
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
            transform.LookAt(attackTarget.transform);
            anim.SetTrigger("Attack");
            attackTarget.Hit(this);
            state = Define.MonsterState.Trace;
        }
        else
        {
            state = Define.MonsterState.Trace;
        }
    }

    protected override void Skill()
    {
        StartCoroutine(SkillCoroutine());
    }

    IEnumerator SkillCoroutine()
    {
        if (!channeling)
        {
            channeling = true;
            anim.SetBool("Move", false);
            anim.SetTrigger("Skill");
            transform.LookAt(attackTarget.transform);

            if (myStat.atkDistance >= Vector3.Distance(transform.position, attackTarget.transform.position))
            {
                attackTarget.Hit(this, Define.DOTType.BURN);
            }

            GameObject go = Instantiate(Resources.Load("SkillParticle/Drake") as GameObject, skill_effect);
            Destroy(go, clip.length);
            yield return new WaitForSeconds(clip.length);
        }

        channeling = false;

        state = Define.MonsterState.Trace;
    }

    protected override void Die()
    {
        base.Die();
        if (!BOSSROOM.clear)
        {
            BOSSROOM.clear = true;
            BOSSROOM.BossClear();
        }
    }
}
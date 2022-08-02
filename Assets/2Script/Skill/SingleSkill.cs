using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSkill : Skill
{

    [SerializeField] protected string skillEffect = null;
    [SerializeField] protected string skillMotion = null;
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected float coefficientValue = 0f;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft)
    {
        waitingTime = coolTime;

        p_anim.SetBool(skillMotion, true);
        yield return null;
       // yield return CheckOnHit(hitBox, enemies, p_isLeft, duration);
        p_anim.SetBool(skillMotion, false);
    }
}

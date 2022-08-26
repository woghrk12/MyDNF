using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF : Skill
{
    [SerializeField] private int numOfProjectile = 0;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, delay[0]);
        ActivateSkill(p_anim, p_isLeft, p_button, projectile[0]);
        yield return PostDelay(p_anim, duration[0], delay[0]);
    }

    protected override void ActivateSkill(Animator p_anim, bool p_isLeft, string p_button, string p_projectile, Vector3? p_pos = null, float p_chargingValue = 1)
    {
        for(int i =0; i < numOfProjectile; i++)
            base.ActivateSkill(p_anim, p_isLeft, p_button, p_projectile, p_pos, p_chargingValue);
    }
}

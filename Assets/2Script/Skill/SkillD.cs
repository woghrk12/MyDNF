using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillD : Skill
{
    [SerializeField] private Charging chargingController = null;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, delay[0], skillMotion[0]);
        yield return chargingController.CheckCharging(p_anim, p_button);
        ActivateSkill(p_anim, p_isLeft, p_button, projectile[0], null, chargingController.ChargingValue);
        yield return PostDelay(p_anim, duration[0], delay[0], skillMotion[0]);
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);
        chargingController.ResetValue(p_anim);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF : Skill
{
    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, delay[0]);
        ActivateSkill(p_anim, p_isLeft, p_button, projectile[0]);
        yield return PostDelay(p_anim, duration[0], delay[0], skillMotion[0]);
    }
}

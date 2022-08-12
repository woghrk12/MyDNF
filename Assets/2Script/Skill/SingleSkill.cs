using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSkill : Skill
{
    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, 0);
        ActivateSkill(p_anim, p_isLeft, p_button, 0);
        yield return PostDelay(p_anim, 0);
    }

}

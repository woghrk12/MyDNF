using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : Skill
{
    [SerializeField] private int numCombo = 0;
    [SerializeField] private ComboInput comboController = null;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        StartCoroutine(comboController.CheckComboInput(p_button));

        var t_cnt = 0;
        while (t_cnt < numCombo)
        {
            yield return PreDelay(p_anim, t_cnt);
            ActivateSkill(p_anim, p_isLeft, p_button, t_cnt);
            yield return PostDelay(p_anim, t_cnt);

            if (comboController.NumOfClick <= ++t_cnt) break;
        }

        comboController.Flag = false;
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);
        comboController.ResetValue();
    }
}

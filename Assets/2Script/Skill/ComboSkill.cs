using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : Skill
{
    [SerializeField] private float maxComboTime = 0f;
    [SerializeField] private int numCombo = 0;
    private int numOfClick = 0;
    private bool flag = false;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        StartCoroutine(CheckComboInput(p_button));

        var t_cnt = 0;

        while (t_cnt < numCombo)
        {
            yield return PreDelay(p_anim, t_cnt);
            ActivateSkill(p_anim, p_isLeft, p_button, t_cnt);
            yield return PostDelay(p_anim, t_cnt);

            if (numOfClick <= ++t_cnt) break;
        }

        flag = false;
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);

        numOfClick = 0;
    }

    private IEnumerator CheckComboInput(string p_button)
    {
        flag = true;
        var t_timer = 0f;
        while (t_timer <= maxComboTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN) numOfClick++;
            if (!flag) break;

            t_timer += Time.deltaTime;
            yield return null;
        }

        numOfClick = 0;
    }
}

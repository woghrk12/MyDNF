using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillS : Skill
{
    [SerializeField] private float reEnterTime = 0f;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, 0);
        ActivateSkill(p_anim, p_isLeft, p_button, 0, p_anim.transform.position + new Vector3(p_isLeft? -5f : 5f, 0f, 0f));
        StartCoroutine(AdditionalControl(p_button, reEnterTime));
        yield return PostDelay(p_anim, 0);
    }

    private IEnumerator AdditionalControl(string p_button, float p_reEnterTime)
    {
        CanUse = false;

        var t_timer = 0f;
        while (t_timer < p_reEnterTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN) break;

            t_timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
        CanUse = true;
    }
}

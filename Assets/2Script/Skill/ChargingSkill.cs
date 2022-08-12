using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingSkill : Skill
{
    [SerializeField] private float maxChargingValue = 0f;
    [SerializeField] private float maxChargingTime = 0f;
    private float chargingValue = 0f;
    private float originMotionSpeed = 0f;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        ApplyCoolTime();
        yield return PreDelay(p_anim, 0);
        yield return CheckCharging(p_anim, p_button);
        ActivateSkill(p_anim, p_isLeft, p_button, 0, chargingValue);
        yield return PostDelay(p_anim, 0);
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);

        chargingValue = 1f;
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }

    private IEnumerator CheckCharging(Animator p_anim, string p_button)
    {
        chargingValue = 1f;

        var t_timer = 0f;

        originMotionSpeed = p_anim.GetFloat("motionSpeed");
        p_anim.SetFloat("motionSpeed", 0f);

        while (t_timer < maxChargingTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.IDLE) break;

            chargingValue = chargingValue < maxChargingValue ? chargingValue + 0.01f : maxChargingValue;
            t_timer += Time.deltaTime;
            yield return null;
        }

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

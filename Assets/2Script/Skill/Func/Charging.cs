using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : MonoBehaviour
{
    [SerializeField] private float maxChargingValue = 0f;
    [SerializeField] private float maxChargingTime = 0f;

    private float chargingValue = 0f;
    public float ChargingValue { get { return chargingValue; } }

    private float originMotionSpeed = 0f;

    public IEnumerator CheckCharging(Animator p_anim, string p_button)
    {
        chargingValue = 1f;

        var t_timer = 0f;

        originMotionSpeed = p_anim.GetFloat("motionSpeed");
        p_anim.SetFloat("motionSpeed", 0f);

        while (t_timer < maxChargingTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.IDLE) break;

            t_timer += Time.deltaTime;
            chargingValue = chargingValue < maxChargingValue ? chargingValue + 0.01f : maxChargingValue;
            yield return null;
        }

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }

    public void ResetValue(Animator p_anim)
    {
        chargingValue = 1f;

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

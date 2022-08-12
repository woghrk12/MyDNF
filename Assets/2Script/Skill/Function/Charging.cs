using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : MonoBehaviour
{
    [SerializeField] private float maxChargingValue = 0f;
    [SerializeField] private float maxChargingTime = 0f;
    [SerializeField] private float chargingSpeed = 0f;
    private float chargingValue = 0f;
    private float originMotionSpeed = 0f;

    public float ChargingValue { get { return chargingValue; } }

    public IEnumerator CheckCharging(Animator p_anim, string p_button)
    {
        chargingValue = 1f;

        originMotionSpeed = p_anim.GetFloat("motionSpeed");
        p_anim.SetFloat("motionSpeed", 0f);

        var t_timer = 0f;
        while(t_timer < maxChargingTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.IDLE) break;

            chargingValue = chargingValue < maxChargingValue ? chargingValue + chargingSpeed : maxChargingValue;
            t_timer += Time.deltaTime;
            yield return null;
        }

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }

    public void ResetValue(Animator p_anim)
    {
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

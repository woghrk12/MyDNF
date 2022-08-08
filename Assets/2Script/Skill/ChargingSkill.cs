using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingSkill : Skill
{
    [SerializeField] private float maxChargingValue = 0f;
    private float chargingValue = 0f;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;

        p_anim.SetBool(skillMotion[0], true);

        yield return CheckCharging(p_anim, p_button);

        yield return new WaitForSeconds(delay[0]);

        var t_projectile = ObjectPoolingManager.SpawnObject(projectile[0], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.StartProjectile(p_anim.transform.position, p_isLeft, chargingValue);
        yield return new WaitForSeconds(duration[0] - delay[0]);

        p_anim.SetBool(skillMotion[0], false);
        
        chargingValue = 1f;
    }

    private IEnumerator CheckCharging(Animator p_anim, string p_button)
    {
        chargingValue = 1f;

        var t_timer = 0f;

        var t_motionSpeed = p_anim.GetFloat("motionSpeed");
        p_anim.SetFloat("motionSpeed", 0f);

        while (t_timer < maxKeyTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.IDLE) break;

            t_timer += Time.deltaTime;
            chargingValue = chargingValue < maxChargingValue ? chargingValue + 0.01f : maxChargingValue;
            yield return null;
        }

        p_anim.SetFloat("motionSpeed", t_motionSpeed);
    }
}

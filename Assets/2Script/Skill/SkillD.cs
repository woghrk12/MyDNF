using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillD : Skill
{
    [SerializeField] private Charging chargingController = null;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;

        p_anim.SetBool(skillMotion[0], true);

        yield return chargingController.CheckCharging(p_anim, p_button);

        yield return new WaitForSeconds(delay[0]);

        var t_projectile = ObjectPoolingManager.SpawnObject(projectile[0], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.Shot(p_anim.transform.position, Vector3.right, p_isLeft, chargingController.ChargingValue);
        
        yield return new WaitForSeconds(duration[0] - delay[0]);

        p_anim.SetBool(skillMotion[0], false);
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);

        chargingController.ResetValue(p_anim);
    }
}


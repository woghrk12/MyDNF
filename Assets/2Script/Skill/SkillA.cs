using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillA : Skill
{
    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;

        p_anim.SetBool(skillMotion[0], true);

        yield return new WaitForSeconds(delay[0]);

        var t_projectile = ObjectPoolingManager.SpawnObject(projectile[0], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.StartProjectile(p_anim.transform.position, p_isLeft);

        yield return new WaitForSeconds(duration[0] - delay[0]);

        p_anim.SetBool(skillMotion[0], false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : Skill
{
    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;
        var t_cnt = 0;

        while (t_cnt < numCombo)
        {
            p_anim.SetBool(skillMotion[t_cnt], true);

            yield return new WaitForSeconds(delay[t_cnt]);

            var t_projectile = ObjectPoolingManager.SpawnObject(projectile[t_cnt], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            t_projectile.StartProjectile(p_anim.transform.position, p_isLeft);

            yield return new WaitForSeconds(duration[t_cnt] - delay[t_cnt]);

            p_anim.SetBool(skillMotion[t_cnt], false);

            t_cnt++;
            if (NumOfClick <= t_cnt) break;
        }
    }
}

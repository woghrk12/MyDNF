using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : Skill
{
    [SerializeField] private ComboInput comboController = null;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;
        
        comboController.Flag = true;
        StartCoroutine(comboController.CheckNumInput(p_button));

        var t_cnt = 0;

        while (t_cnt < comboController.NumCombo)
        {
            p_anim.SetBool(skillMotion[t_cnt], true);

            yield return new WaitForSeconds(delay[t_cnt]);

            var t_projectile = ObjectPoolingManager.SpawnObject(projectile[t_cnt], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            t_projectile.Shot(p_anim.transform.position, Vector3.right, p_isLeft);

            yield return new WaitForSeconds(duration[t_cnt] - delay[t_cnt]);

            p_anim.SetBool(skillMotion[t_cnt], false);

            t_cnt++;
            if (comboController.NumOfClick <= t_cnt) break;
        }

        comboController.Flag = false;
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);

        comboController.ResetValue();
    }
}

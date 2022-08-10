using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : Skill
{
    private bool flag = false;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button)
    {
        waitingTime = coolTime;

        flag = true;
        StartCoroutine(CheckNumInput(p_button, MaxKeyTime));
        
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

        flag = false;
    }

    private IEnumerator CheckNumInput(string p_button, float p_maxKeyTime)
    {
        var t_timer = 0f;

        while (t_timer <= p_maxKeyTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN) NumOfClick++;
            if (!flag) break;

            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    public override void ResetSkill(Animator p_anim)
    {
        base.ResetSkill(p_anim);

        NumOfClick = 0;
    }
}

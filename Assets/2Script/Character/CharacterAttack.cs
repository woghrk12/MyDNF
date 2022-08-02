using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft) => UseSkillCo(p_skill, p_isLeft);
    private IEnumerator UseSkillCo(Skill p_skill, bool p_isLeft)
    {
        StartCoroutine(p_skill.UseSkill(p_isLeft));
        
        yield return new WaitForSeconds(p_skill.Delay);
    }

    public IEnumerator InputCombo(ComboSkill p_skill, string p_buttonName) => InputComboCo(p_skill, p_buttonName);
    private IEnumerator InputComboCo(ComboSkill p_skill, string p_buttonName)
    {
        p_skill.IsAttack = true;

        var t_timer = 0f;

        while (t_timer < p_skill.MaxComboTime)
        {
            if (Input.GetButtonDown(p_buttonName)) p_skill.NumOfClicks++;
            if (!p_skill.IsAttack) break;
            t_timer += Time.deltaTime;
            yield return null;
        }

        p_skill.NumOfClicks = 0;
    }
}

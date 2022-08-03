using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    private bool flag = false;

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft, KeyCode p_button)
    {
        flag = true;

        StartCoroutine(CheckNumInput(p_skill, p_button, p_skill.MaxKeyTime));
        yield return p_skill.UseSkill(anim, p_isLeft);
        p_skill.NumOfClick = 0;
        flag = false;
    }

    private IEnumerator CheckNumInput(Skill p_skill, KeyCode p_button, float p_maxKeyTime)
    {
        var t_timer = 0f;

        while (t_timer <= p_maxKeyTime)
        {
            if (Input.GetKeyDown(p_button)) p_skill.NumOfClick++;
            if (!flag) break; 
            
            t_timer += Time.deltaTime;
            yield return null;
        }
    }
}

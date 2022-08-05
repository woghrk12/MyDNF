using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private SpriteRenderer originSprite = null;
    [SerializeField] private SpriteRenderer cancelEffect = null;

    private bool flag = false;
    public Skill runningSkill = null;

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft, string p_button)
    {
        flag = true;
        runningSkill = p_skill;
        StartCoroutine(CheckNumInput(p_skill, p_button, p_skill.MaxKeyTime));
        yield return p_skill.UseSkill(anim, p_isLeft);
        
        p_skill.NumOfClick = 0;
        runningSkill = null;
        flag = false;
    }

    private IEnumerator CheckNumInput(Skill p_skill, string p_button, float p_maxKeyTime)
    {
        var t_timer = 0f;

        while (t_timer <= p_maxKeyTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN) p_skill.NumOfClick++;
            if (!flag) break; 
            
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    public void CancelSkill(Skill p_skill)
    {
        p_skill.NumOfClick = 0;
        p_skill.ResetSkillMotion(anim);
        runningSkill = null;
        flag = false;

        StartCoroutine(CancelEffect());
    }

    private IEnumerator CancelEffect()
    {
        cancelEffect.sprite = originSprite.sprite;

        cancelEffect.enabled = true;
        yield return new WaitForSeconds(0.08f);
        cancelEffect.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private SpriteRenderer originSprite = null;
    [SerializeField] private SpriteRenderer cancelEffect = null;

    public Skill runningSkill = null;

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft, string p_button)
    {
        runningSkill = p_skill;
        
        yield return p_skill.UseSkill(anim, p_isLeft, p_button);
        
        p_skill.NumOfClick = 0;
        runningSkill = null;
    }

    public void CancelSkill(Skill p_skill)
    {
        p_skill.NumOfClick = 0;
        p_skill.ResetSkillMotion(anim);
        runningSkill = null;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private SpriteRenderer originSprite = null;
    [SerializeField] private SpriteRenderer cancelEffect = null;

    private Skill runningSkill = null;
    public Skill RunningSkill { get { return runningSkill; } }

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft, string p_button, bool p_notCancel)
    {
        if (!p_notCancel) runningSkill = p_skill;

        yield return p_skill.UseSkill(anim, p_isLeft, p_button);

        if(!p_notCancel) runningSkill = null;
    }

    public void UseSkillWithoutCancel(Skill p_skill, bool p_isLeft)
        => StartCoroutine(p_skill.UseSkill(anim, p_isLeft));

    public void CancelSkill(Skill p_skill)
    {
        p_skill.ResetSkill(anim);
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

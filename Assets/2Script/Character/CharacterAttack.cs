using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private float attackSpeed = 1f;
    public float AttackSpeed { set { attackSpeed = value < 1f ? 1f : value; } get { return attackSpeed; } }

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft) => UseSkillCo(p_skill, p_isLeft);
    private IEnumerator UseSkillCo(Skill p_skill, bool p_isLeft)
    {
        StartCoroutine(p_skill.UseSkill(p_isLeft));
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(p_skill.Delay);
    }

}

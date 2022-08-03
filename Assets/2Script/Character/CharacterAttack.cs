using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    public IEnumerator UseSkill(Skill p_skill, bool p_isLeft, string p_button)
    {
        yield return p_skill.UseSkill(anim, p_isLeft);
    }
}

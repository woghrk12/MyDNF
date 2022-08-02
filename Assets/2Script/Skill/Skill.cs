using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillType { SINGLE, COMBO, END }

public abstract class Skill : MonoBehaviour
{
    public ESkillType skillType = ESkillType.SINGLE;

    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;

    public bool CanUse { get { return waitingTime <= 0f; } }

    protected void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public abstract IEnumerator UseSkill(Animator p_anim, bool p_isLeft);
}


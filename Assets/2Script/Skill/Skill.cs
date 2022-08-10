using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected int numCombo = 1;
    [SerializeField] protected string[] projectile = null;
    [SerializeField] protected string[] skillMotion = null;
    [SerializeField] protected float[] duration = null;
    [SerializeField] protected float[] delay = null;
    [SerializeField] protected float[] coefficientValue = null;
    [SerializeField] protected float maxKeyTime = 0f;
    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;

    public List<Skill> CanCancelList = new List<Skill>();
    public bool CanUse { get { return waitingTime <= 0f; } }
    public float MaxKeyTime { get { return maxKeyTime; } }

    protected void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public abstract IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button);

    public virtual void ResetSkill(Animator p_anim)
    {
        for (int i = 0; i < skillMotion.Length; i++)
            p_anim.SetBool(skillMotion[i], false);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected string[] projectile = null;
    [SerializeField] protected string[] skillMotion = null;
    [SerializeField] protected float[] duration = null;
    [SerializeField] protected float[] delay = null;
    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;

    public List<Skill> CanCancelList = new List<Skill>();
    public bool CanUse { get { return waitingTime <= 0f; } }

    protected void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public abstract IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button);

    protected void ApplyCoolTime() { waitingTime = coolTime; }

    protected IEnumerator PreDelay(Animator p_anim, int p_cnt)
    {
        p_anim.SetBool(skillMotion[p_cnt], true);

        yield return new WaitForSeconds(delay[p_cnt]);
    }

    protected void ActivateSkill(Animator p_anim, bool p_isLeft, string p_button, int p_cnt, float p_chargingValue = 1f)
    {
        var t_projectile = ObjectPoolingManager.SpawnObject(projectile[p_cnt], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.Shot(p_anim.transform.position, p_button, p_isLeft, p_chargingValue);
    }

    protected IEnumerator PostDelay(Animator p_anim, int p_cnt)
    {
        yield return new WaitForSeconds(duration[p_cnt] - delay[p_cnt]);

        p_anim.SetBool(skillMotion[p_cnt], false);
    }

    public virtual void ResetSkill(Animator p_anim)
    {
        for (int i = 0; i < skillMotion.Length; i++)
            p_anim.SetBool(skillMotion[i], false);
    }
}


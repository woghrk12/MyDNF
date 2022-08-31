using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private int needMana = 0;
    [SerializeField] protected string[] projectile = null;
    [SerializeField] protected string[] skillMotion = null;
    [SerializeField] protected float[] duration = null;
    [SerializeField] protected float[] delay = null;
    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;
    public int WaitingTime { get { return Mathf.CeilToInt(waitingTime); } }
    private bool canUse = true;
    [SerializeField] private bool canUseWithoutCancel = false;

    public List<Skill> CanCancelList = new List<Skill>();
    public bool CanUse { set { canUse = value; } get { return waitingTime <= 0f && canUse; } }
    public bool CanUseWithoutCancel { get { return canUseWithoutCancel; } }
    public int NeedMana { get { return needMana; } }

    protected void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public abstract IEnumerator UseSkill(Animator p_anim, bool p_isLeft, string p_button = null);

    protected void ApplyCoolTime() { waitingTime = coolTime; }

    protected IEnumerator PreDelay(Animator p_anim, float p_delay, string p_skillMotion = null)
    {
        if (p_skillMotion != null) p_anim.SetBool(p_skillMotion, true);

        yield return new WaitForSeconds(p_delay);
    }

    protected virtual void ActivateSkill(Animator p_anim, bool p_isLeft, string p_button, string p_projectile, Vector3? p_pos = null, float p_chargingValue = 1f)
    {
        var t_projectile = ObjectPoolingManager.SpawnObject(p_projectile, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.Shot(p_pos.HasValue ? p_pos.Value : p_anim.transform.position, p_button, p_isLeft, p_chargingValue);
    }

    protected IEnumerator PostDelay(Animator p_anim, float p_duration, float p_delay, string p_skillMotion = null)
    {
        yield return new WaitForSeconds(p_duration - p_delay);

        if (p_skillMotion != null) p_anim.SetBool(p_skillMotion, false);
    }

    public virtual void ResetSkill(Animator p_anim)
    {
        for (int i = 0; i < skillMotion.Length; i++)
            p_anim.SetBool(skillMotion[i], false);
    }
}


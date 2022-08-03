using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSkill : Skill
{
    [SerializeField] protected string projectile = null;
    [SerializeField] protected string skillMotion = null;
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected float coefficientValue = 0f;

    public override IEnumerator UseSkill(Animator p_anim, bool p_isLeft)
    {
        waitingTime = coolTime;

        p_anim.SetTrigger(skillMotion);
        var t_projectile = ObjectPoolingManager.SpawnObject(projectile, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.StartProjectile(p_anim.transform.position, p_isLeft);
        yield return new WaitForSeconds(duration);
    }
}

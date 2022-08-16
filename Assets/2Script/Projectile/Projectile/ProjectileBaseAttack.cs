using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileBaseAttack : Projectile
{
    [SerializeField] private InstanceHit hitController = null;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        yield return ActivateProjectile(duration);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_duration, float p_timesValue = 1f)
    {
        StartCoroutine(hitController.CheckOnHit(coefficient, duration, hitBox, enemies));
        StartCoroutine(MoveProjectile(direction, duration));

        yield return new WaitForSeconds(p_duration);
    }
}



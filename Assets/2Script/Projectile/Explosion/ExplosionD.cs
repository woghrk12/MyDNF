using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionD : Projectile
{
    [SerializeField] private InstanceHit hitController = null;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);
        yield return new WaitForSeconds(duration);
        hitController.StopCheckOnHit();
        EndProjectile();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileA : Projectile
{
    [SerializeField] private ContinuousHit hitController = null;
    [SerializeField] private MoveProjectile moveController = null;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        moveController.SetDirection(p_isLeft);
        StartProjectile();
        yield return ActivateProjectile();
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_timesValue = 1f)
    {
        moveController.Move(duration, startSpeed);
        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);
        yield return new WaitForSeconds(duration);
    }
}

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
        StartProjectile();
        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);
        yield return moveController.LerpMove(hitBox, duration, startSpeed, p_isLeft ? Vector3.left : Vector3.right, false);
        EndProjectile();
    }

    protected override void EndProjectile()
    {
        hitController.StopCheckOnHit();
        base.EndProjectile();
    }
}

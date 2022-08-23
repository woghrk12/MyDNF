using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileA : Projectile
{
    [SerializeField] private ContinuousHit hitController = null;
    [SerializeField] private MoveProjectile moveController = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        targets = roomManager.Enemies.ToList();
    }

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        moveController.SetDirection(p_isLeft);
        StartProjectile();
        yield return ActivateProjectile(duration);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_duration, float p_timesValue = 1f)
    {
        StartCoroutine(hitController.CheckOnHit(coefficient, duration, transform, yPosObject, hitBox, targets));
        moveController.Move(p_duration);

        yield return new WaitForSeconds(p_duration);
    }
}

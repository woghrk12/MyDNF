using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileF : Projectile
{
    [SerializeField] private InstanceHit hitController = null;
    [SerializeField] private MoveProjectile moveController = null;
    private float originYPos = 0f;

    private Coroutine runningCo = null;
    private HitBox target = null;

    protected override void Awake()
    {
        base.Awake();

        originYPos = hitBox.YPos;
    }

    protected override void InitializeValue()
    {
        base.InitializeValue();

        hitBox.YPos = originYPos;
        target = null;
    }

    protected override void SetProjectile(Vector3 p_position, bool p_isLeft, float p_sizeEff)
    {
        base.SetProjectile(p_position, p_isLeft, p_sizeEff);

        target = targets.Count != 0 ? targets[Random.Range(0, targets.Count - 1)] : null;
    }

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();

        yield return moveController.LerpMove(
            transformController,
            1f + Random.Range(0.5f, 1f),
            Random.Range(1.5f, 3f),
            (p_isLeft ? Vector3.right : Vector3.left) + Vector3.up + Random.insideUnitSphere,
            true
            );

        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);

        if(target != null)
            yield return moveController.ConstantMove(
                transformController,
                duration,
                startSpeed,
                target.TargetPos - (hitBox.ObjectPos + Random.insideUnitSphere * 0.5f)
                );

        EndProjectile();
    }

    protected override void EndProjectile()
    {
        hitController.StopCheckOnHit();
        base.EndProjectile();
    }

}

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

        target = targets[Random.Range(0, targets.Count - 1)];
    }

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        yield return ShotProjectile(p_isLeft);

        runningCo = StartCoroutine(ActivateProjectile());
        yield return MoveProjectile(p_isLeft);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_timesValue = 1)
    {
        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator ShotProjectile(bool p_isLeft)
    {
        var t_timer = 0f;
        var t_waitingTime = 1f + Random.Range(0.5f, 1f);
        while (t_timer < t_waitingTime)
        {
            hitBox.ObjectPos += ((p_isLeft ? Vector3.right : Vector3.left) + Vector3.up + Random.insideUnitSphere * 5f) * Random.Range(0.5f, 1.5f) * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveProjectile(bool p_isLeft)
    {
        var t_timer = 0f;
        var t_dir = (target.ObjectPos - (hitBox.ObjectPos + Random.insideUnitSphere * 0.5f)).normalized;

        while (t_timer < duration)
        {
            hitBox.ObjectPos += t_dir * 20f * Time.deltaTime;
            if (hitBox.YPos <= 0) 
            {
                hitController.StopCheckOnHit();
                StopCoroutine(runningCo);
                break;
            }
            t_timer += Time.deltaTime;
            yield return null;
        }
    }
}

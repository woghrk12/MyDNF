using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileF : Projectile
{
    [SerializeField] private InstanceHit hitController = null;
    [SerializeField] private MoveProjectile moveController = null;
    [SerializeField] private Vector3 originYPos = Vector3.zero;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        StartCoroutine(MoveProjectile(p_isLeft));
        yield return ActivateProjectile();
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_timesValue = 1)
    {
        StartCoroutine(hitController.CheckOnHit(coefficient, duration, hitBox, targets));

        yield return new WaitForSeconds(duration);
    }

    private IEnumerator MoveProjectile(bool p_isLeft)
    {
        var t_timer = 0f;
        
        while (t_timer < 2f)
        {
            hitBox.ObjectPos += ((p_isLeft ? Vector3.right : Vector3.left) + Vector3.up) * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }

        t_timer = 0f;
        var t_target = targets[Random.Range(0, targets.Count - 1)];
        var t_dir = (t_target.ObjectPos - hitBox.ObjectPos).normalized;

        while (t_timer < duration)
        {
            hitBox.ObjectPos += t_dir * 20f * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }
}

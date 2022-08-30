using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileD : Projectile
{
    [SerializeField] private InstanceHit hitController = null;
    [SerializeField] private MoveProjectile moveController = null;
    [SerializeField] private float canExplosion = 0f;
    [SerializeField] private string explosion = null;
    private bool explosionFlag = false;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        StartCoroutine(moveController.LerpMove(hitBox, duration, startSpeed, p_isLeft ? Vector3.left : Vector3.right, true));
        yield return ActivateProjectile(p_sizeEff);
        explosionFlag = CheckExplosion(p_sizeEff);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_timesValue = 1f)
    {
        hitController.StartCheckOnHit((int)(coefficient * p_timesValue), duration, hitBox, targets);
        yield return new WaitForSeconds(duration);
    }

    private bool CheckExplosion(float p_value) { return p_value >= canExplosion; }

    protected override void EndProjectile()
    {
        if (explosionFlag)
        {
            var t_explosion = ObjectPoolingManager.SpawnObject(explosion, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            t_explosion.Shot(transform.position);
        }

        base.EndProjectile();
    }
}

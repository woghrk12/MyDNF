using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileS : Projectile
{
    [SerializeField] private string explosion = null;

    protected override void EndProjectile()
    {
        var t_explosion = ObjectPoolingManager.SpawnObject(explosion, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_explosion.Shot(transform.position, Vector3.zero);

        base.EndProjectile();
    }
}

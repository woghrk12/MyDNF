using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileF : Projectile
{
    protected override IEnumerator ActivateProjectile(float p_timesValue = 1)
    {
        yield return null;
    }

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        yield return null;
    }
}

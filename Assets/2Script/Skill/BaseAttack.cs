using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : Projectile
{
    protected override IEnumerator MoveProjectile(bool p_isLeft, float p_duration)
    {
        yield return null;
    }
}

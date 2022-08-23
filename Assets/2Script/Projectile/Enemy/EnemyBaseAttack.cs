using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAttack : EnemyProjectile
{
    [SerializeField] private InstanceHit hitController = null;

    protected override IEnumerator ActivateProjectile()
    {
        StartCoroutine(hitController.CheckOnHit(coefficient, duration, transform, yPosObject, hitBox, targets));
        yield return new WaitForSeconds(duration);
    }
}

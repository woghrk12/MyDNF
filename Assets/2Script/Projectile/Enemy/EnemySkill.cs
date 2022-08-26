using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : EnemyProjectile
{
    [SerializeField] private ContinuousHit hitController = null;
    public override void SetProjectile(bool p_isLeft)
    {
        transform.position = targets[0].transform.position;
        hitBox.IsLeft = p_isLeft;
    }

    protected override IEnumerator ActivateProjectile()
    {
        StartCoroutine(hitController.CheckOnHit(coefficient, duration, hitBox, targets));
        yield return new WaitForSeconds(duration);
        ObjectPoolingManager.ReturnObject(gameObject);
    }
}

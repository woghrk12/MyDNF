using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private Damagable healthController = null;
    [SerializeField] private EnemyAttack attackController = null;

    private void OnEnable()
    {
        hitBox.OnDamageEvent += healthController.OnDamage;
    }

    private void OnDisable()
    {
        hitBox.OnDamageEvent -= healthController.OnDamage;
    }

    private void Update()
    {
        hitBox.CalculateHitBox();
    }

}

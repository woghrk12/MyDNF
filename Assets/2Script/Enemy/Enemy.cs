using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private Damagable healthController = null;

    private void OnEnable()
    {
        hitBox.OnDamageEvent += OnDamage;
    }

    private void OnDisable()
    {
        hitBox.OnDamageEvent -= OnDamage;
    }

    private void Update()
    {
        hitBox.CalculateHitBox();
    }

    private void OnDamage(int p_damage)
    {
        healthController.OnDamage(p_damage);
    }
}

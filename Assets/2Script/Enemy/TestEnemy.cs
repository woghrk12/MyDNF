using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;

    private void OnEnable()
    {
        hitBox.OnDamageEvent += OnDamage;
        Debug.Log(hitBox.OnDamageEvent);
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
        Debug.Log(p_damage);
    }
}

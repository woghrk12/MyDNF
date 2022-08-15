using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int maxHealth = 0;
    private int curHealth = 0;

    private Coroutine runningCo = null;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void OnDamage(int p_damage)
    {
        curHealth -= p_damage;

        if (runningCo != null) StopCoroutine(runningCo);

        runningCo = StartCoroutine(OnDamageCo());
    }

    private IEnumerator OnDamageCo()
    {
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private string baseAttackMotion = null;
    [SerializeField] private float basePreDelay = 0f;
    [SerializeField] private float baseDuration = 0f;
    [SerializeField] private string skillAttackMotion = null;
    [SerializeField] private float skillPreDelay = 0f;
    [SerializeField] private float skillDuration = 0f;

    [SerializeField] private string baseAttackProjectile = null;
    [SerializeField] private string skillProjectile = null;

    private float originMotionSpeed = 0f;
    private Coroutine runningCo = null;

    private void Awake()
    {
        originMotionSpeed = anim.GetFloat("motionSpeed");
    }

    public void StartPattern(Animator p_anim, EEnemyPatternType p_attackMotion) => runningCo = StartCoroutine(Attack(p_anim, p_attackMotion)); 

    private IEnumerator Attack(Animator p_anim, EEnemyPatternType p_attackMotion)
    {
        p_anim.SetBool("isAttack", true);

        switch (p_attackMotion)
        {
            case EEnemyPatternType.BASEATTACK:
                yield return AttackCo(p_anim, baseAttackMotion, baseAttackProjectile, basePreDelay, baseDuration);
                break;
            case EEnemyPatternType.SKILL:
                yield return AttackCo(p_anim, skillAttackMotion, skillProjectile, skillPreDelay, skillDuration);
                break;
        }

        p_anim.SetBool("isAttack", false);
        runningCo = null;
    }

    private IEnumerator AttackCo(Animator p_anim, string p_attackMotion, string p_projectile, float p_preDelay, float p_duration)
    {
        p_anim.SetTrigger(p_attackMotion);
        p_anim.SetFloat("motionSpeed", 0f);

        SpawnProjectile(p_projectile);

        yield return new WaitForSeconds(p_preDelay);

        p_anim.SetFloat("motionSpeed", originMotionSpeed);

        yield return new WaitForSeconds(p_duration);
    }

    private void SpawnProjectile(string p_projectile)
    {
        ObjectPoolingManager.SpawnObject(p_projectile, Vector3.zero, Quaternion.identity);
    }

    public void CancelAttack(Animator p_anim)
    {
        if (runningCo == null) return;

        StopCoroutine(runningCo);
        p_anim.SetBool("isAttack", false);
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

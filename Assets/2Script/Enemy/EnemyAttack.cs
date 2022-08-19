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

    private void Awake()
    {
        originMotionSpeed = anim.GetFloat("motionSpeed");
    }

    public IEnumerator AttackPattern(Animator p_anim, Transform p_target, EEnemyPatternType p_attackMotion) => SelectAttack(p_anim, p_target, p_attackMotion); 

    private IEnumerator SelectAttack(Animator p_anim, Transform p_target, EEnemyPatternType p_attackMotion)
    {
        p_anim.SetBool("isAttack", true);

        
        var t_localScale = transform.localScale;
        t_localScale.x = p_target.transform.position.x > transform.position.x ? -1f : 1f;
        transform.localScale = t_localScale;

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

    public void ResetValue(Animator p_anim)
    {
        p_anim.SetBool("isAttack", false);
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

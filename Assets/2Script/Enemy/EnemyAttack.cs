using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private string baseAttackMotion = null;
    [SerializeField] private float baseWaiting = 0f;
    [SerializeField] private float basePreDelay = 0f;
    [SerializeField] private float baseDuration = 0f;
    [SerializeField] private string skillAttackMotion = null;
    [SerializeField] private float skillWaiting = 0f;
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

        var t_isLeft = p_target.transform.position.x < transform.position.x;
        var t_localScale = transform.localScale;
        t_localScale.x = t_isLeft ? -1f : 1f;
        transform.localScale = t_localScale;

        switch (p_attackMotion)
        {
            case EEnemyPatternType.BASEATTACK:
                yield return AttackCo(p_anim, baseAttackMotion, baseAttackProjectile, t_isLeft, baseWaiting, basePreDelay, baseDuration);
                break;
            case EEnemyPatternType.SKILL:
                yield return AttackCo(p_anim, skillAttackMotion, skillProjectile, t_isLeft, skillWaiting, skillPreDelay, skillDuration);
                break;
        }

        p_anim.SetBool("isAttack", false);
    }

    private IEnumerator AttackCo(Animator p_anim, string p_attackMotion, string p_projectile, bool p_isLeft, float p_waiting, float p_preDelay, float p_duration)
    {
        var t_projectile = ObjectPoolingManager.SpawnObject(p_projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();

        p_anim.SetTrigger(p_attackMotion);
        p_anim.SetFloat("motionSpeed", 0f);
        t_projectile.SetProjectile(p_isLeft);

        yield return new WaitForSeconds(p_waiting);
        
        p_anim.SetFloat("motionSpeed", originMotionSpeed);

        yield return new WaitForSeconds(p_preDelay);

        t_projectile.Shot();

        yield return new WaitForSeconds(p_duration);
    }

    public void ResetValue(Animator p_anim)
    {
        p_anim.SetBool("isAttack", false);
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

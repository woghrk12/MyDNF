using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private float basePreDelay = 0f;
    [SerializeField] private float baseDuration = 0f;
    [SerializeField] private float skillPreDelay = 0f;
    [SerializeField] private float skillDuration = 0f;

    [SerializeField] private float[] attackProbs = null;

    private float originMotionSpeed = 0f;
    private Coroutine runningCo = null;

    private void Awake()
    {
        originMotionSpeed = anim.GetFloat("motionSpeed");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            StartCoroutine(Attack(anim));
    }

    public IEnumerator Attack(Animator p_anim)
    {
        p_anim.SetBool("isAttack", true);

        var t_attackMotion = ChooseAttackMotion(attackProbs);
        switch (t_attackMotion)
        {
            case 0:
                yield return BaseAttack(p_anim);
                break;
            case 1:
                yield return SkillAttack(p_anim);
                break;
        }

        p_anim.SetBool("isAttack", false);
    }

    private int ChooseAttackMotion(float[] p_probs)
    {
        var t_total = 0f;

        foreach (var t_elem in p_probs)
        {
            t_total += t_elem;
        }

        var t_random = Random.value * t_total;

        for (int i = 0; i < p_probs.Length; i++)
        {
            if (t_random < p_probs[i]) return i;
            else t_random -= p_probs[i];
        }

        return p_probs.Length - 1;
    }

    private IEnumerator BaseAttack(Animator p_anim)
    {
        p_anim.SetTrigger("BaseAttack");

        yield return SpawnProjectile(p_anim, "EnemyBaseAttack", basePreDelay, baseDuration);
    }

    private IEnumerator SkillAttack(Animator p_anim)
    {
        p_anim.SetTrigger("SkillAttack");

        yield return SpawnProjectile(p_anim, "EnemyBaseAttack", skillPreDelay, skillDuration);
    }

    private IEnumerator SpawnProjectile(Animator p_anim, string p_projectile, float p_preDelay, float p_duration)
    {
        p_anim.SetFloat("motionSpeed", 0f);
     
        yield return new WaitForSeconds(p_preDelay);

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
        ObjectPoolingManager.SpawnObject(p_projectile, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(p_duration);
    }

    public void CancelAttack(Animator p_anim)
    {
        if (runningCo != null) StopCoroutine(runningCo);
        p_anim.SetBool("isAttack", false);
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

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

        var t_cnt = Random.Range(0, 10);
        switch (t_cnt)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                yield return SkillAttack(p_anim);
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                yield return BaseAttack(p_anim);
                break;
        }

        p_anim.SetBool("isAttack", false);
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

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

        p_anim.SetFloat("motionSpeed", 0f);

        yield return new WaitForSeconds(basePreDelay);

        p_anim.SetFloat("motionSpeed", originMotionSpeed);
        ObjectPoolingManager.SpawnObject("EnemyBaseAttack", Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(baseDuration);

        p_anim.SetBool("isAttack", false);
    }

    public void CancelAttack(Animator p_anim)
    {
        if (runningCo != null) StopCoroutine(runningCo);
        p_anim.SetBool("isAttack", false);
        p_anim.SetFloat("motionSpeed", originMotionSpeed);
    }
}

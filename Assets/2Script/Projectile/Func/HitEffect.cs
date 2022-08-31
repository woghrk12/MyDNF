using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    public void StartHitEffect() => StartCoroutine(HitEffectCo());

    private IEnumerator HitEffectCo()
    {
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(0.4f);
        ObjectPoolingManager.ReturnObject(gameObject);
    }
}

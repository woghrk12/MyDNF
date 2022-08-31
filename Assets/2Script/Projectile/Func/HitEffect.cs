using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Transform yPosObject = null;

    public void StartHitEffect() => StartCoroutine(HitEffectCo());

    private IEnumerator HitEffectCo()
    {
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(0.4f);
        ObjectPoolingManager.ReturnObject(gameObject);
    }

    public void SetPosition(HitBox p_hitBox, HitBox p_target)
    {
        transform.position = new Vector3((p_hitBox.XPos + p_target.XPos) * 0.5f, (p_hitBox.ZPos + p_target.ZPos) * 0.5f, 0f);
        yPosObject.localPosition = new Vector3(0f, p_hitBox.YPos, 0f);
    }
}

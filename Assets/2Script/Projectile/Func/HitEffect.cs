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
        var t_xPos = Mathf.Clamp(p_hitBox.XPos, p_target.MinHitBox.x, p_target.MaxHitBox.x);
        var t_yPos = Mathf.Clamp(p_hitBox.YTargetPos, p_target.MinHitBox.y, p_target.MaxHitBox.y);
        var t_zPos = Mathf.Clamp(p_hitBox.ZPos, p_target.MinHitBox.z, p_target.MaxHitBox.z);
        
        transform.position = new Vector3(t_xPos, t_zPos, 0f);
        yPosObject.localPosition = new Vector3(0f, t_yPos, 0f);
    }
}

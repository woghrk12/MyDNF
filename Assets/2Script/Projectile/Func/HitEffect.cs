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
        var t_xPos = Mathf.Clamp(p_hitBox.XPos, p_target.MinHitBoxX, p_target.MaxHitBoxX);
        var t_yPos = Mathf.Clamp(p_hitBox.YPos, p_target.MinHitBoxY, p_target.MaxHitBoxY);
        var t_zPos = Mathf.Clamp(p_hitBox.ZPos, p_target.MinHitBoxZ, p_target.MaxHitBoxZ);
        
        transform.position = new Vector3(t_xPos, t_zPos, 0f);
        yPosObject.localPosition = new Vector3(0f, t_yPos, 0f);
    }
}

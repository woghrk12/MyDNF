using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float sizeRightX = 0f;
    [SerializeField] private float sizeLeftX = 0f;
    [SerializeField] private float sizeUpY = 0f;
    [SerializeField] private float sizeDownY = 0f;
    [SerializeField] private float sizeZ = 0f;

    private float rangeRightX = 0f;
    private float rangeLeftX = 0f;
    private float rangeUpY = 0f;
    private float rangeDownY = 0f;
    private float radiusZ = 0f;
    
    private float minHitBoxX = 0f;
    private float maxHitBoxX = 0f;
    private float minHitBoxY = 0f;
    private float maxHitBoxY = 0f;
    private float minHitBoxZ = 0f;
    private float maxHitBoxZ = 0f;

    private UnityAction<int, Vector3, float, float> onDamageEvent = null;
    public UnityAction<int, Vector3, float, float> OnDamageEvent { set { onDamageEvent = value; } get { return onDamageEvent; } }

    private void OnEnable()
    {
        rangeLeftX = sizeLeftX;
        rangeRightX = sizeRightX;
        rangeUpY = sizeUpY;
        rangeDownY = sizeDownY;
        radiusZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox(Transform p_posObj, Transform p_yPosObj = null)
    {
        minHitBoxX = p_posObj.position.x - rangeLeftX;
        maxHitBoxX = p_posObj.position.x + rangeRightX;
        minHitBoxZ = p_posObj.position.y - radiusZ;
        maxHitBoxZ = p_posObj.position.y + radiusZ;

        if (p_yPosObj == null) return;

        minHitBoxY = p_yPosObj.localPosition.y - rangeDownY;
        maxHitBoxY = p_yPosObj.localPosition.y + rangeUpY;
    }

    public void SetDirection(bool p_isLeft)
    {
        var t_left = rangeLeftX; var t_right = rangeRightX;

        rangeLeftX = p_isLeft ? t_right : t_left;
        rangeRightX = p_isLeft ? t_left : t_right;
    }

    public void ScaleHitBox(float p_value)
    {
        rangeLeftX *= p_value;
        rangeRightX *= p_value;
        rangeDownY *= p_value;
        rangeUpY *= p_value;
        radiusZ *= p_value;
    }

    public bool CalculateOnHit(HitBox p_target)
    {
        if (maxHitBoxX < p_target.minHitBoxX || minHitBoxX > p_target.maxHitBoxX) return false;
        if (maxHitBoxZ < p_target.minHitBoxZ || minHitBoxZ > p_target.maxHitBoxZ) return false;
        if (maxHitBoxY < p_target.minHitBoxY || minHitBoxY > p_target.maxHitBoxY) return false;

        return true;
    }
}

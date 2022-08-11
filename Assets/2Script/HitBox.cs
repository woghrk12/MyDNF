using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Transform spriteObject = null;

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
    
    [HideInInspector] public float minHitBoxX = 0f;
    [HideInInspector] public float maxHitBoxX = 0f;
    [HideInInspector] public float minHitBoxY = 0f;
    [HideInInspector] public float maxHitBoxY = 0f;
    [HideInInspector] public float minHitBoxZ = 0f;
    [HideInInspector] public float maxHitBoxZ = 0f;

    private void OnEnable()
    {
        rangeLeftX = sizeLeftX;
        rangeRightX = sizeRightX;
        rangeUpY = sizeUpY;
        rangeDownY = sizeDownY;
        radiusZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox()
    {
        minHitBoxX = transform.position.x - rangeLeftX;
        maxHitBoxX = transform.position.x + rangeRightX;
        minHitBoxZ = transform.position.y - radiusZ;
        maxHitBoxZ = transform.position.y + radiusZ;
        minHitBoxY = spriteObject.localPosition.y - rangeDownY;
        maxHitBoxY = spriteObject.localPosition.y + rangeUpY;
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

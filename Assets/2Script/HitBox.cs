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
    private float radiusZ = 0f;
    /*
    [HideInInspector] public float minHitBoxX = 0f;
    [HideInInspector] public float maxHitBoxX = 0f;
    [HideInInspector] public float minHitBoxY = 0f;
    [HideInInspector] public float maxHitBoxY = 0f;
    [HideInInspector] public float minHitBoxZ = 0f;
    [HideInInspector] public float maxHitBoxZ = 0f;
    */
    public float minHitBoxX = 0f;
    public float maxHitBoxX = 0f;
    public float minHitBoxY = 0f;
    public float maxHitBoxY = 0f;
    public float minHitBoxZ = 0f;
    public float maxHitBoxZ = 0f;

    private void Awake()
    {
        rangeLeftX = sizeLeftX;
        rangeRightX = sizeRightX;
        radiusZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox()
    {
        minHitBoxX = transform.position.x - rangeLeftX;
        maxHitBoxX = transform.position.x + rangeRightX;
        minHitBoxZ = transform.position.y - radiusZ;
        maxHitBoxZ = transform.position.y + radiusZ;
        minHitBoxY = spriteObject.localPosition.y - sizeDownY;
        maxHitBoxY = spriteObject.localPosition.y + sizeUpY;
    }

    public void SetDirection(bool p_isLeft)
    {
        rangeLeftX = p_isLeft ? sizeRightX : sizeLeftX;
        rangeRightX = p_isLeft ? sizeLeftX : sizeRightX;
    }
}

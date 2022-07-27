using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Transform spriteObject = null;

    [SerializeField] private float sizeX = 0f;
    [SerializeField] private float sizeUpY = 0f;
    [SerializeField] private float sizeDownY = 0f;
    [SerializeField] private float sizeZ = 0f;

    public float minHitBoxX = 0f;
    public float maxHitBoxX = 0f;
    public float minHitBoxY = 0f;
    public float maxHitBoxY = 0f;
    public float minHitBoxZ = 0f;
    public float maxHitBoxZ = 0f;

    public void Update()
    {
        minHitBoxX = transform.position.x - sizeX;
        maxHitBoxX = transform.position.x + sizeX;
        minHitBoxZ = transform.position.y - sizeZ;
        maxHitBoxZ = transform.position.y + sizeZ;
        minHitBoxY = spriteObject.localPosition.y - sizeDownY;
        maxHitBoxY = spriteObject.localPosition.y + sizeUpY;
    }
}

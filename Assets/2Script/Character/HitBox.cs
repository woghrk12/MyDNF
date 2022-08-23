using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float sizeX = 0f;
    [SerializeField] private float sizeY = 0f;
    [SerializeField] private float sizeZ = 0f;

    [SerializeField] private bool isCenterX = false;
    [SerializeField] private bool isCenterY = false;

    private float halfX = 0f;
    private float halfY = 0f;
    private float halfZ = 0f;

    private float minHitBoxX = 0f;
    private float maxHitBoxX = 0f;
    private float minHitBoxY = 0f;
    private float maxHitBoxY = 0f;
    private float minHitBoxZ = 0f;
    private float maxHitBoxZ = 0f;

    private bool isLeft = false;
    public bool IsLeft { set { isLeft = value; } get { return isLeft; } }

    private UnityAction<int, Vector3, float, float> onDamageEvent = null;
    public UnityAction<int, Vector3, float, float> OnDamageEvent { set { onDamageEvent = value; } get { return onDamageEvent; } }

    private void OnEnable()
    {
        halfX = sizeX * 0.5f;
        halfY = sizeY * 0.5f;
        halfZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox(Vector3 p_posObj, Vector3? p_yPosObj = null)
    {
        if (isCenterX) { minHitBoxX = p_posObj.x - halfX; maxHitBoxX = p_posObj.x + halfX; }
        else
        {
            if (isLeft) { minHitBoxX = p_posObj.x - sizeX; maxHitBoxX = p_posObj.x; }
            else { minHitBoxX = p_posObj.x; maxHitBoxX = p_posObj.x + sizeX; }
        }
    
        minHitBoxZ = p_posObj.y - halfZ;
        maxHitBoxZ = p_posObj.y + halfZ;

        if (p_yPosObj == null) return;

        if (isCenterY) { minHitBoxY = p_yPosObj.Value.y - halfY; maxHitBoxY = p_yPosObj.Value.y + halfY; }
        else { minHitBoxY = p_yPosObj.Value.y; maxHitBoxY = p_yPosObj.Value.y + sizeY; }
    }

    public void ScaleHitBox(float p_value)
    {
        sizeX *= p_value;
        sizeY *= p_value;
        sizeZ *= p_value;
    }

    public bool CalculateOnHit(HitBox p_target)
    {
        if (maxHitBoxX < p_target.minHitBoxX || minHitBoxX > p_target.maxHitBoxX) return false;
        if (maxHitBoxZ < p_target.minHitBoxZ || minHitBoxZ > p_target.maxHitBoxZ) return false;
        if (maxHitBoxY < p_target.minHitBoxY || minHitBoxY > p_target.maxHitBoxY) return false;

        return true;
    }
}

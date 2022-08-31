using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Transform posObject = null;
    [SerializeField] private Transform yPosObject = null;

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

    public float XPos 
    {
        set 
        {
            var t_pos = posObject.position;
            t_pos.x = value;
            posObject.position = t_pos;
        }
        get { return posObject.position.x; } 
    }
    public float YPos 
    {
        set 
        {
            var t_yPos = yPosObject.localPosition;
            t_yPos.y = value;
            yPosObject.localPosition = t_yPos;
        }
        get { return yPosObject.localPosition.y; } 
    }
    public float ZPos 
    {
        set
        {
            var t_pos = posObject.position;
            t_pos.y = value;
            posObject.position = t_pos;
        }
        get { return posObject.position.y; }
    }
    public Vector3 ObjectPos 
    {
        set 
        {
            var t_pos = posObject.position;
            t_pos.x = value.x; t_pos.y = value.z;
            posObject.position = t_pos;

            if (yPosObject != null)
            {
                var t_yPos = yPosObject.localPosition;
                t_yPos.y = value.y;
                yPosObject.localPosition = t_yPos;
            }
            
        } 
        get { return new Vector3(XPos, yPosObject != null ? YPos : 0f, ZPos); } 
    }
    public Vector3 TargetPos
    {
        get { return new Vector3((minHitBoxX + maxHitBoxX) * 0.5f, (minHitBoxY + maxHitBoxY) * 0.5f, (minHitBoxZ + maxHitBoxZ) * 0.5f); }    
    }

    private void OnEnable()
    {
        halfX = sizeX * 0.5f;
        halfY = sizeY * 0.5f;
        halfZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox()
    {
        if (isCenterX) { minHitBoxX = ObjectPos.x - halfX; maxHitBoxX = ObjectPos.x + halfX; }
        else
        {
            if (isLeft) { minHitBoxX = ObjectPos.x - sizeX; maxHitBoxX = ObjectPos.x; }
            else { minHitBoxX = ObjectPos.x; maxHitBoxX = ObjectPos.x + sizeX; }
        }
    
        minHitBoxZ = ObjectPos.z - halfZ;
        maxHitBoxZ = ObjectPos.z + halfZ;

        if (yPosObject == null) return;

        if (isCenterY) { minHitBoxY = ObjectPos.y - halfY; maxHitBoxY = ObjectPos.y + halfY; }
        else { minHitBoxY = ObjectPos.y; maxHitBoxY = ObjectPos.y + sizeY; }
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

        if (yPosObject == null) return true;
        
        if (maxHitBoxY < p_target.minHitBoxY || minHitBoxY > p_target.maxHitBoxY) return false;
        
        return true;
    }
}

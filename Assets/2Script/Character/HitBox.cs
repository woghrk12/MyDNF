using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] private CharacterTransform charTr = null;

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
    public float MinHitBoxX { get { return minHitBoxX; } }
    public float MaxHitBoxX { get { return maxHitBoxX; } }
    public float MinHitBoxY { get { return minHitBoxY; } }
    public float MaxHitBoxY { get { return maxHitBoxY; } }
    public float MinHitBoxZ { get { return minHitBoxZ; } }
    public float MaxHitBoxZ { get { return maxHitBoxZ; } }


    private bool isLeft = false;
    public bool IsLeft { set { isLeft = value; } get { return isLeft; } }

    private UnityAction<int, Vector3, float, float> onDamageEvent = null;
    public UnityAction<int, Vector3, float, float> OnDamageEvent { set { onDamageEvent = value; } get { return onDamageEvent; } }

    public float XPos { set { charTr.XPos = value; } get { return charTr.XPos; } }
    public float YPos { set { charTr.YPos = value; } get { return charTr.YPos; } }
    public float ZPos { set { charTr.ZPos = value; } get { return charTr.ZPos; } }
    public Vector3 ObjectPos { set { charTr.Position = value; } get { return charTr.Position; } }
 
    public float XTargetPos { get { return (minHitBoxX + maxHitBoxX) * 0.5f; } }
    public float YTargetPos { get { return (minHitBoxY + maxHitBoxY) * 0.5f; } }
    public float ZTargetPos { get { return (minHitBoxZ + maxHitBoxZ) * 0.5f; } }
    public Vector3 TargetPos
    {
        get { return new Vector3(XTargetPos, YTargetPos, ZTargetPos); }    
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

        if (!charTr.HasYObj) return;

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

        if (!charTr.HasYObj) return true;
        
        if (maxHitBoxY < p_target.minHitBoxY || minHitBoxY > p_target.maxHitBoxY) return false;
        
        return true;
    }
}

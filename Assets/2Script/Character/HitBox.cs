using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    private static readonly int xRate = 16;
    private static readonly int yRate = 9;
    private static readonly float convRate = (float)yRate / xRate;
    private static readonly float invConvRate = (float)xRate / yRate;

    private CharacterTransform charTr = null;

    [SerializeField] private float sizeX = 0f;
    [SerializeField] private float sizeY = 0f;
    [SerializeField] private float sizeZ = 0f;

    [SerializeField] private bool isCenterX = false;
    [SerializeField] private bool isCenterY = false;

    private float halfX = 0f;
    private float halfY = 0f;
    private float halfZ = 0f;

    private Vector3 minHitBox = Vector3.zero;
    private Vector3 maxHitBox = Vector3.zero;
    public Vector3 MinHitBox { get { return minHitBox; } }
    public Vector3 MaxHitBox { get { return maxHitBox; } }

    private bool isLeft = false;
    public bool IsLeft { set { isLeft = value; } get { return isLeft; } }

    private UnityAction<int, Vector3, float, float> onDamageEvent = null;
    public UnityAction<int, Vector3, float, float> OnDamageEvent { set { onDamageEvent = value; } get { return onDamageEvent; } }

    public float XPos { set { charTr.XPos = value; } get { return charTr.XPos; } }
    public float YPos { set { charTr.YPos = value; } get { return charTr.YPos; } }
    public float ZPos { set { charTr.ZPos = value; } get { return charTr.ZPos; } }
    public Vector3 ObjectPos { set { charTr.Position = value; } get { return charTr.Position; } }
 
    public float XTargetPos { get { return (minHitBox.x + maxHitBox.x) * 0.5f; } }
    public float YTargetPos { get { return (minHitBox.y + maxHitBox.y) * 0.5f; } }
    public float ZTargetPos { get { return (minHitBox.z + maxHitBox.z) * 0.5f; } }
    public Vector3 TargetPos
    {
        get { return new Vector3(XTargetPos, YTargetPos, ZTargetPos); }    
    }

    private void OnEnable()
    {
        charTr = GetComponent<CharacterTransform>();
        halfX = sizeX * 0.5f;
        halfY = sizeY * 0.5f;
        halfZ = sizeZ * 0.5f;
    }

    public void CalculateHitBox()
    {
        if (isCenterX) { minHitBox.x = ObjectPos.x - halfX; maxHitBox.x = ObjectPos.x + halfX; }
        else
        {
            if (isLeft) { minHitBox.x = ObjectPos.x - sizeX; maxHitBox.x = ObjectPos.x; }
            else { minHitBox.x = ObjectPos.x; maxHitBox.x = ObjectPos.x + sizeX; }
        }
    
        minHitBox.z = ObjectPos.z - halfZ;
        maxHitBox.z = ObjectPos.z + halfZ;

        if (!charTr.HasYObj) return;

        if (isCenterY) { minHitBox.y = ObjectPos.y - halfY; maxHitBox.y = ObjectPos.y + halfY; }
        else { minHitBox.y = ObjectPos.y; maxHitBox.y = ObjectPos.y + sizeY; }
    }

    public void ScaleHitBox(float p_value)
    {
        sizeX *= p_value;
        sizeY *= p_value;
        sizeZ *= p_value;
    }

    public bool CalculateOnHit(HitBox p_target)
    {
        var t_minHitBox = ConvertCoord(minHitBox); var t_maxHitBox = ConvertCoord(maxHitBox);
        var t_targetMin = ConvertCoord(p_target.MinHitBox); var t_targetMax = ConvertCoord(p_target.MaxHitBox);

        if (t_maxHitBox.x < t_targetMin.x || t_minHitBox.x > t_targetMax.x) return false;
        if (t_maxHitBox.z < t_targetMin.z || t_minHitBox.z > t_targetMax.z) return false;

        if (!charTr.HasYObj) return true;
        
        if (t_maxHitBox.y < t_targetMin.y || t_minHitBox.y > t_targetMax.y) return false;
        
        return true;
    }

    public Vector3 ConvertCoord(Vector3 p_vector)
    {
        var t_vector = p_vector;
        t_vector.y *= convRate;  
        return t_vector;
    }
}

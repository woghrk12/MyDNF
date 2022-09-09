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

    [SerializeField] private EHitBoxType hitBoxType = EHitBoxType.BOX;
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
        halfZ = ((hitBoxType == EHitBoxType.BOX ? sizeZ : sizeX) * 0.5f) * convRate;
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
        if (hitBoxType == p_target.hitBoxType)
        {
            return hitBoxType == EHitBoxType.BOX
                ? CheckBB(this, p_target)
                : CheckCC(this, p_target);
        }

        return hitBoxType == EHitBoxType.BOX
            ? CheckBC(this, p_target)
            : CheckBC(p_target, this);
    }

    private Vector3 ConvertCoord(Vector3 p_vector)
    {
        var t_vector = p_vector;
        t_vector.z *= invConvRate;  
        return t_vector;
    }

    private bool CheckBB(HitBox p_boxFrom, HitBox p_boxTo)
    {
        var t_minHitBox = ConvertCoord(p_boxFrom.minHitBox); var t_maxHitBox = ConvertCoord(p_boxFrom.maxHitBox);
        var t_targetMin = ConvertCoord(p_boxTo.minHitBox); var t_targetMax = ConvertCoord(p_boxTo.maxHitBox);

        if (t_maxHitBox.x < t_targetMin.x || t_minHitBox.x > t_targetMax.x) return false;
        if (t_maxHitBox.z < t_targetMin.z || t_minHitBox.z > t_targetMax.z) return false;

        if (!p_boxFrom.charTr.HasYObj) return true;

        if (t_maxHitBox.y < t_targetMin.y || t_minHitBox.y > t_targetMax.y) return false;

        return true;
    }

    private bool CheckInsideCircle(Vector3 p_center, float p_radius, float p_x, float p_z)
    {
        return (p_x - p_center.x) * (p_x - p_center.x) + (p_z - p_center.z) * (p_z - p_center.z) < p_radius * p_radius;
    }

    private bool CheckBC(HitBox p_box, HitBox p_circle)
    {
        var t_minBox = ConvertCoord(p_box.minHitBox); var t_maxBox = ConvertCoord(p_box.maxHitBox);
        var t_minCircle = ConvertCoord(p_circle.minHitBox); var t_maxCircle = ConvertCoord(p_circle.maxHitBox);

        var t_boxCenter = (t_minBox + t_maxBox) * 0.5f;
        var t_circleCenter= (t_minCircle + t_maxCircle) * 0.5f;

        var t_rectNum = ((t_circleCenter.x < t_minBox.x) ? 0 : (t_circleCenter.x > t_maxBox.x) ? 2 : 1)
            + 3 * ((t_circleCenter.z < t_minBox.z) ? 0 : (t_circleCenter.z > t_maxBox.z) ? 2 : 1);

        switch (t_rectNum)
        {
            case 1:
            case 7:
                if ((t_maxBox.z - t_minBox.z) * 0.5f + p_circle.halfX < Mathf.Abs(t_boxCenter.z - t_circleCenter.z)) return false;
                break;
            case 3:
            case 5:
                if ((t_maxBox.x - t_minBox.x) * 0.5f + p_circle.halfX < Mathf.Abs(t_boxCenter.x - t_circleCenter.x)) return false;
                break;
            default:
                var t_cornerX = (t_rectNum == 0 || t_rectNum == 6) ? t_minBox.x : t_maxBox.x;
                var t_cornerZ = (t_rectNum == 0 || t_rectNum == 2) ? t_minBox.z : t_maxBox.z;
                if (!CheckInsideCircle(t_circleCenter, p_circle.halfX, t_cornerX, t_cornerZ)) return false;
                break;
        }

        if (!p_box.charTr.HasYObj) return true;

        if (t_maxBox.y < t_minCircle.y || t_minBox.y > t_maxCircle.y) return false;

        return true;
    }

    private bool CheckCC(HitBox p_circleFrom, HitBox p_circleTo)
    {
        var t_minHitBox = ConvertCoord(p_circleFrom.minHitBox); var t_maxHitBox = ConvertCoord(p_circleFrom.maxHitBox);
        var t_targetMin = ConvertCoord(p_circleTo.minHitBox); var t_targetMax = ConvertCoord(p_circleTo.maxHitBox);

        var t_center = (t_minHitBox + t_maxHitBox) * 0.5f;
        var t_targetCenter = (t_targetMin + t_targetMax) * 0.5f;
        var t_dist = t_center - t_targetCenter;
        t_dist.y = 0f;

        if ((p_circleFrom.halfX + p_circleTo.halfX) * (p_circleFrom.halfX + p_circleTo.halfX) < t_dist.sqrMagnitude) return false;

        if (!p_circleFrom.charTr.HasYObj) return true;

        if (t_maxHitBox.y < t_targetMin.y || t_minHitBox.y > t_targetMax.y) return false;

        return true;
    }
}

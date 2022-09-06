using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransform : MonoBehaviour
{
    private static readonly int xRate = 16;
    private static readonly int yRate = 9;
    private static readonly float convRate = (float)yRate / xRate;
    private static readonly float invConvRate = (float)xRate / yRate;

    [SerializeField] private Transform posObj = null;
    [SerializeField] private Transform yPosObj = null;

    public float XPos
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.x = value;
            posObj.position = t_pos;
        }
        get { return posObj.position.x; }
    }
    public float YPos
    {
        set
        {
            var t_yPos = yPosObj.localPosition;
            t_yPos.y = value;
            yPosObj.localPosition = t_yPos;
        }
        get { return yPosObj.localPosition.y; }
    }
    public float ZPos
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.y = value * convRate;
            posObj.position = t_pos;
        }
        get { return posObj.position.y * invConvRate; }
    }
    public Vector3 Position
    {
        set
        {
            XPos = value.x;
            ZPos = value.z;

            if (yPosObj != null)
            {
                YPos = value.y;
            }
        }
        get { return new Vector3(XPos, HasYObj ? YPos : 0f, ZPos); }
    }
    public bool HasYObj { get { return yPosObj != null; } }
}

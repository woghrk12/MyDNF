using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterVector3
{
    private static readonly int xRate = 16;
    private static readonly int yRate = 9;
    private static readonly float conversionRate = yRate / xRate;
    private Vector3 conversionVector3;

    public float XPos { set { conversionVector3.x = value; } get { return conversionVector3.x; } }
    public float YPos { set { conversionVector3.y = value; } get { return conversionVector3.y; } }
    public float ZPos { set { conversionVector3.z = value * conversionRate; } get { return conversionVector3.z; } }
    public Vector3 Position { set { XPos = value.x; YPos = value.y; ZPos = value.z; } get { return conversionVector3; } }
}

public class CharacterTransform : MonoBehaviour
{
    [SerializeField] private Transform posObj = null;
    [SerializeField] private Transform yPosObj = null;
    private CharacterVector3 objTransform;

    public float XPos 
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.x = objTransform.XPos = value;
            posObj.position = t_pos;
        }
        get { return objTransform.XPos; }
    }
    public float YPos
    {
        set
        {
            var t_yPos = yPosObj.localPosition;
            t_yPos.y = objTransform.YPos = value;
            yPosObj.localPosition = t_yPos;
        }
        get { return objTransform.YPos; }
    }
    public float ZPos
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.y = objTransform.ZPos = value;
            posObj.position = t_pos;
        }
        get { return objTransform.ZPos; }
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
        get { return objTransform.Position; }
    }
}

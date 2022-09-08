using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransform : MonoBehaviour
{
    [SerializeField] private Transform posObj = null;
    [SerializeField] private Transform yPosObj = null;
    [SerializeField] private Transform scaleObj = null;

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
            t_pos.y = value;
            posObj.position = t_pos;
        }
        get { return posObj.position.y; }
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
    public Vector3 LocalScale { set { scaleObj.localScale = value; } get { return scaleObj.localScale; } }
}

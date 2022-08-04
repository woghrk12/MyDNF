using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 Direction { set { inputDir = value; } get { return inputDir; } }
    public bool JDown { set { jDown = value; } get { return jDown; } }
    public bool XDown { set { xDown = value; } get { return xDown; } }
    public bool ADown { set { aDown = value; } get { return aDown; } }
    public bool SDown { set { sDown = value; } get { return sDown; } }

    private Vector2 inputDir = Vector2.zero;
    private bool jDown = false;
    private bool xDown = false;
    private bool aDown = false;
    private bool sDown = false;
}

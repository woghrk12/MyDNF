using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenInputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick = null;

    public Vector2 Direction { get { return joystick.Direction; } }
    public bool JDown { set { jDown = value; } get { return jDown; } }
    public bool XDown { set { xDown = value; } get { return xDown; } }
    public bool ADown { set { aDown = value; } get { return aDown; } }
    public bool SDown { set { sDown = value; } get { return sDown; } }

    private bool jDown = false;
    private bool xDown = false;
    private bool aDown = false;
    private bool sDown = false;

    public void OnClickJButton() { JDown = true; }
    public void OnClickXButton() { XDown = true; }
    public void OnClickAButton() { ADown = true; }
    public void OnClickSButton() { SDown = true; }

    private void LateUpdate()
    {
        JDown = false;
        XDown = false;
        ADown = false;
        SDown = false;
    }
}

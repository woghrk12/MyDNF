using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenInputManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private Joystick joystick = null;

    public void OnClickJButton() { inputManager.JDown = true; }
    public void OnClickXButton() { inputManager.XDown = true; }
    public void OnClickAButton() { inputManager.ADown = true; }
    public void OnClickSButton() { inputManager.SDown = true; }

    private void Update()
    {
        inputManager.Direction = joystick.Direction;
    }

    private void LateUpdate()
    {
        inputManager.JDown = false;
        inputManager.XDown = false;
        inputManager.ADown = false;
        inputManager.SDown = false;
    }
}

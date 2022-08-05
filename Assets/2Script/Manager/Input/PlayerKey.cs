using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] private string buttonName = null;
    private bool isPressed = false;
    private bool onPressed = false;

    private void Update()
    {
        if (Input.GetButtonDown(buttonName)) isPressed = true;
        if (Input.GetButtonUp(buttonName)) isPressed = false;

        if (isPressed)
        {
            InputManager.Buttons[buttonName] = onPressed ? EButtonState.PRESSED : EButtonState.DOWN;
            onPressed = true;
        }
        else
        {
            InputManager.Buttons[buttonName] = onPressed ? EButtonState.UP : EButtonState.IDLE;
            onPressed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string buttonName = null;
    private bool isPressed = false;
    private bool onPressed = false;

    public void OnPointerDown(PointerEventData eventData) { isPressed = true; }
    public void OnPointerUp(PointerEventData eventData) { isPressed = false; }

    void Update()
    {
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

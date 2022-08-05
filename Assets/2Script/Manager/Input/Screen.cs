using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private Joystick joystick = null;
    [SerializeField] private PlayerButton[] buttons = null;

    private void Update()
    {
        inputManager.Direction = joystick.Direction;
    }
}

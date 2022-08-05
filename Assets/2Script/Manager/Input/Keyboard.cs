using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    private float hAxis = 0f;
    private float vAxis = 0f;

    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode xButton = KeyCode.X;
    [SerializeField] private KeyCode aButton = KeyCode.A;
    [SerializeField] private KeyCode sButton = KeyCode.S;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        inputManager.JDown = Input.GetKeyDown(jumpButton);
        inputManager.XDown = Input.GetKeyDown(xButton);
        inputManager.ADown = Input.GetKeyDown(aButton);
        inputManager.SDown = Input.GetKeyDown(sButton);

        inputManager.Direction = Vector3.ClampMagnitude(new Vector3(hAxis, vAxis, 0f), 1f);
    }
}

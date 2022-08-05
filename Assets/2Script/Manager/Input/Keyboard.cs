using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private PlayerKey[] playerKey = null;

    private float hAxis = 0f;
    private float vAxis = 0f;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
        inputManager.Direction = Vector3.ClampMagnitude(new Vector3(hAxis, vAxis, 0f), 1f);
    }
}

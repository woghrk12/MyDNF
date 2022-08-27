using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private PlayerKey[] playerKey = null;

    private float hAxis = 0f;
    private float vAxis = 0f;

    [SerializeField] private ControlSlider healthSlider = null;
    [SerializeField] private ControlSlider manaSlider = null;

    [SerializeField] private Status playerStatus = null;

    private void OnEnable()
    {
        playerStatus.SetControlSlider(healthSlider, manaSlider);
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
        inputManager.Direction = Vector3.ClampMagnitude(new Vector3(hAxis, 0f, vAxis), 1f);
    }
}

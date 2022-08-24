using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private Joystick joystick = null;
    [SerializeField] private PlayerButton[] buttons = null;

    [SerializeField] private ControlSlider healthSlider = null;
    [SerializeField] private ControlSlider manaSlider = null;

    [SerializeField] private Status playerStatus = null;

    private void OnEnable()
    {
        playerStatus.SetControlSlider(healthSlider, manaSlider);
    }

    private void Update()
    {
        inputManager.Direction = joystick.Direction;
    }
}

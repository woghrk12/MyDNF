using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlType { KEYBOARD, SCREEN }

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject keyboardUI = null;
    [SerializeField] private GameObject screenUI = null;

    private void Start()
    {
        ChangeControlType(GameManager.Instance.ControlType);
    }

    private void ChangeControlType(EControlType p_type)
    {
        switch (p_type)
        {
            case EControlType.KEYBOARD:
                keyboardUI.SetActive(true);
                screenUI.SetActive(false);
                break;
            case EControlType.SCREEN:
                keyboardUI.SetActive(false);
                screenUI.SetActive(true);
                break;
        }
    }
}

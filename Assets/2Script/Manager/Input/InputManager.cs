using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EButtonState { IDLE, DOWN, PRESSED, UP }

public class InputManager : MonoBehaviour
{
    public Vector3 Direction { set { inputDir = value; } get { return inputDir; } }
    public string JButton { set { jButton = value; } get { return jButton; } }
    public string XButton { set { xButton = value; } get { return xButton; } }
    public string AButton { set { aButton = value; } get { return aButton; } }
    public string SButton { set { sButton = value; } get { return sButton; } }
    public string DButton { set { dButton = value; } get { return dButton; } }
    public string FButton { set { fButton = value; } get { return fButton; } }

    public static Dictionary<string, EButtonState> Buttons { get { return buttons; } }
    private static Dictionary<string, EButtonState> buttons = null;

    private Vector3 inputDir = Vector3.zero;

    [SerializeField] private string jButton = "Jump";
    [SerializeField] private string xButton = "X";
    [SerializeField] private string aButton = "A";
    [SerializeField] private string sButton = "S";
    [SerializeField] private string dButton = "D";
    [SerializeField] private string fButton = "F";
    private EButtonState jState = EButtonState.IDLE;
    private EButtonState xState = EButtonState.IDLE;
    private EButtonState aState = EButtonState.IDLE;
    private EButtonState sState = EButtonState.IDLE;
    private EButtonState dState = EButtonState.IDLE;
    private EButtonState fState = EButtonState.IDLE;

    private void Awake()
    {
        buttons = new Dictionary<string, EButtonState>();

        Buttons.Add(jButton, jState);
        Buttons.Add(xButton, xState);
        Buttons.Add(aButton, aState);
        Buttons.Add(sButton, sState);
        Buttons.Add(dButton, dState);
        Buttons.Add(fButton, fState);
    }

    public bool GetButtonDown(string p_buttonName) { return buttons[p_buttonName] == EButtonState.DOWN; }
    public bool GetButtonPressed(string p_buttonName) { return buttons[p_buttonName] != EButtonState.IDLE; }
    public bool GetButtonUp(string p_buttonName) { return buttons[p_buttonName] == EButtonState.UP; }
}

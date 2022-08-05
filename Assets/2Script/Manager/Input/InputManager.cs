using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EButtonState { IDLE, DOWN, PRESSED, UP }

public class InputManager : MonoBehaviour
{
    public Vector2 Direction { set { inputDir = value; } get { return inputDir; } }
    public bool JDown { set { jDown = value; } get { return jDown; } }
    public bool XDown { set { xDown = value; } get { return xDown; } }
    public bool ADown { set { aDown = value; } get { return aDown; } }
    public bool SDown { set { sDown = value; } get { return sDown; } }

    private Vector2 inputDir = Vector2.zero;
    private bool jDown = false;
    private bool xDown = false;
    private bool aDown = false;
    private bool sDown = false;

    public static Dictionary<string, EButtonState> Buttons { get { return buttons; } }
    private static Dictionary<string, EButtonState> buttons = null;

    [SerializeField] private string jButton = "Jump";
    [SerializeField] private string xButton = "X";
    [SerializeField] private string aButton = "A";
    [SerializeField] private string sButton = "S";
    private EButtonState jState = EButtonState.IDLE;
    private EButtonState xState = EButtonState.IDLE;
    private EButtonState aState = EButtonState.IDLE;
    private EButtonState sState = EButtonState.IDLE;

    private void Awake()
    {
        buttons = new Dictionary<string, EButtonState>();

        Buttons.Add(jButton, jState);
        Buttons.Add(xButton, xState);
        Buttons.Add(aButton, aState);
        Buttons.Add(sButton, sState);
    }
}

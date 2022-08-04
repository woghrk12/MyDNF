using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public Vector2 Direction { get { return new Vector2(inputDir.x, inputDir.y); } }
    public bool JDown { get { return jDown; } }
    public bool XDown { get { return xDown; } }
    public bool ADown { get { return aDown; } }
    public bool SDown { get { return sDown; } }

    public KeyCode JumpButton { get { return jumpButton; } }
    public KeyCode XButton { get { return xButton; } }
    public KeyCode AButton { get { return aButton; } }
    public KeyCode SButton { get { return sButton; } }


    private Vector2 inputDir = Vector2.zero;
    private float hAxis = 0f;
    private float vAxis = 0f;

    private bool jDown = false;
    private bool xDown = false;
    private bool aDown = false;
    private bool sDown = false;
    

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
        
        jDown = Input.GetKeyDown(jumpButton);
        xDown = Input.GetKeyDown(xButton);
        aDown = Input.GetKeyDown(aButton);
        sDown = Input.GetKeyDown(sButton);

        inputDir = Vector3.ClampMagnitude(new Vector3(hAxis, vAxis, 0f), 1f);
    }
}

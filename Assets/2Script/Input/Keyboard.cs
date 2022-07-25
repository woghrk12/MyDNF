using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public Vector2 Direction { get { return new Vector2(inputDir.x, inputDir.y); } }

    private Vector2 inputDir = Vector2.zero;
    private float hAxis = 0f;
    private float vAxis = 0f;

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        inputDir = Vector3.ClampMagnitude(new Vector3(hAxis, vAxis, 0f), 1f);
    }
}

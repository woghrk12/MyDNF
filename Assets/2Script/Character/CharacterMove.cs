using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private float hAxis = 0f;
    private float vAxis = 0f;

    [SerializeField] private float xMoveSpeed = 0f;
    [SerializeField] private float yMoveSpeed = 0f;
    private Vector3 moveDir = Vector3.zero;

    public bool isMove { get { return moveDir != Vector3.zero; } }

    private bool isLeft = false;
    public bool IsLeft
    {
        set
        {
            isLeft = value;
            transform.localScale = new Vector3(isLeft ? -1f : 1f, 1f, 1f);
        }
        get { return isLeft; }
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private Vector3 HandleInput(Vector3 p_vector)
    {
        p_vector.x *= xMoveSpeed;
        p_vector.y *= yMoveSpeed;
        return p_vector;
    }

    private void Move()
    {
        moveDir = HandleInput(Vector3.ClampMagnitude(new Vector3(hAxis, vAxis, 0f), 1f));

        transform.position += moveDir * Time.deltaTime;
        anim.SetBool("isWalk", isMove);

        if (moveDir.x != 0)
        {
            IsLeft = moveDir.x < 0;
        }
    }
}

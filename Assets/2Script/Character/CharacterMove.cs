using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private float xMoveSpeed = 0f;
    [SerializeField] private float yMoveSpeed = 0f;
    private Vector3 moveDir = Vector3.zero;

    [SerializeField] private float minX = 0f, maxX = 0f;
    [SerializeField] private float minY = 0f, maxY = 0f;

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

    private bool canMove = true;
    public bool CanMove 
    {
        set 
        {
            canMove = value;
            if (!canMove)
            {
                moveDir = Vector3.zero;
                anim.SetBool("isWalk", false);
            }
        }
        get { return canMove; }
    }

    public void Move(Vector3 p_moveDir) => MoveCharacter(p_moveDir);

    private Vector3 HandleInput(Vector3 p_vector)
    {
        p_vector.x *= xMoveSpeed;
        p_vector.y *= yMoveSpeed;
        return p_vector;
    }

    private Vector3 LimitArea(Vector3 t_playerPos)
    {
        Vector3 t_pos = t_playerPos;

        if (t_pos.x < minX) t_pos.x = minX;
        if (t_pos.x > maxX) t_pos.x = maxX;
        if (t_pos.y < minY) t_pos.y = minY;
        if (t_pos.y > maxY) t_pos.y = maxY;

        return t_pos;
    }

    private void MoveCharacter(Vector3 p_moveDir)
    {
        moveDir = HandleInput(p_moveDir);
        var t_pos = transform.position + moveDir * Time.deltaTime;
        transform.position = LimitArea(t_pos);

        anim.SetBool("isWalk", isMove);

        if (p_moveDir.x != 0)
        {
            IsLeft = p_moveDir.x < 0;
        }
    }
}

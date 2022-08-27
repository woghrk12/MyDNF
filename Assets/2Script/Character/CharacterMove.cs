using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private float xMoveSpeed = 0f;
    [SerializeField] private float zMoveSpeed = 0f;
    private Vector3 moveDir = Vector3.zero;

    [SerializeField] private float minX = 0f, maxX = 0f;
    [SerializeField] private float minZ = 0f, maxZ = 0f;

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

    public void Move(HitBox p_hitBox, Vector3 p_moveDir) => MoveCharacter(p_hitBox, p_moveDir);

    private Vector3 HandleInput(Vector3 p_vector)
    {
        var t_vector = p_vector;
        t_vector.x *= xMoveSpeed;
        t_vector.z *= zMoveSpeed;
        return t_vector;
    }

    private Vector3 LimitArea(Vector3 p_pos)
    {
        Vector3 t_pos = p_pos;

        if (t_pos.x < minX) t_pos.x = minX;
        if (t_pos.x > maxX) t_pos.x = maxX;
        if (t_pos.z < minZ) t_pos.z = minZ;
        if (t_pos.z > maxZ) t_pos.z = maxZ;

        return t_pos;
    }

    private void MoveCharacter(HitBox p_hitBox, Vector3 p_moveDir)
    {
        moveDir = HandleInput(p_moveDir);
        var t_pos = p_hitBox.ObjectPos + moveDir * Time.deltaTime;
        p_hitBox.ObjectPos = LimitArea(t_pos);

        anim.SetBool("isWalk", isMove);

        if (p_moveDir.x != 0)
        {
            IsLeft = p_moveDir.x < 0;
        }
    }
}

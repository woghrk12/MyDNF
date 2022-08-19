using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private float xMoveSpeed = 0f;
    [SerializeField] private float yMoveSpeed = 0f;
    private Vector3 moveDir = Vector3.zero;

    [SerializeField] private float minX = 0f, maxX = 0f;
    [SerializeField] private float minY = 0f, maxY = 0f;

    private void FixedUpdate()
    {
        LimitArea();
    }

    public void MoveCharacter(Vector3 p_moveDir) => Move(p_moveDir);

    private void LimitArea()
    {
        var t_pos = transform.position;

        if (t_pos.x < minX) t_pos.x = minX;
        if (t_pos.x > maxX) t_pos.x = maxX;
        if (t_pos.y < minY) t_pos.y = minY;
        if (t_pos.y > maxY) t_pos.y = maxY;

        transform.position = t_pos;
    }

    private Vector3 HandleInput(Vector3 p_vector)
    {
        var t_vector = p_vector.normalized;
        t_vector.x *= xMoveSpeed;
        t_vector.y *= yMoveSpeed;
        return t_vector;
    }

    private void Move(Vector3 p_moveDir)
    {
        moveDir = HandleInput(p_moveDir);
        var t_pos = transform.position + moveDir * Time.deltaTime;

        anim.SetBool("isWalk", moveDir != Vector3.zero);
    }
}

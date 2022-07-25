using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float minX = 0f, maxX = 0f;
    [SerializeField] private float minY = 0f, maxY = 0f;

    private void LateUpdate()
    {
        Follow(target);
    }

    private Vector3 LimitArea(Vector3 p_cameraPos)
    {
        Vector3 t_pos = p_cameraPos;

        if (t_pos.x < minX) t_pos.x = minX;
        if (t_pos.x > maxX) t_pos.x = maxX;
        if (t_pos.y < minY) t_pos.y = minY;
        if (t_pos.y > maxY) t_pos.y = maxY;

        return t_pos;
    }

    private void Follow(Transform p_target)
    {
        Vector3 t_pos = p_target.transform.position + offset;
        transform.position = LimitArea(t_pos);
    }
}

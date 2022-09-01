using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxXPos = 0f;
    private Vector2 startPos = Vector2.zero;
    private float newPos = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.position.x >= maxXPos) transform.position = startPos;

        transform.position += Time.deltaTime * Vector3.right * speed;
    }
}

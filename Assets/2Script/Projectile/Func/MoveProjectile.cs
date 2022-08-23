using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] private Vector3 rawDirection = Vector3.zero;
    [SerializeField] private bool boostFlag = false;
    [SerializeField] private float startSpeed = 0f;
    private Vector3 direction = Vector3.zero;

    public void SetDirection(bool p_isLeft)
    {
        direction = new Vector3(p_isLeft ? -rawDirection.x : rawDirection.x, rawDirection.y, rawDirection.z).normalized;
    }

    public void Move(float p_duration) => StartCoroutine(MoveCo(p_duration));

    private IEnumerator MoveCo(float p_duration)
    {
        var t_timer = 0f;
        var t_speed = boostFlag ? startSpeed : 0f;
        while (t_timer < p_duration)
        {
            t_speed = boostFlag
                ? Mathf.Lerp(startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, startSpeed, t_timer / p_duration);
            transform.position += direction * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
        
    }
}

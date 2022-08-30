using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] private Vector3 rawDirection = Vector3.zero;
    [SerializeField] private bool boostFlag = false;
    private Vector3 direction = Vector3.zero;

    public void SetDirection(bool p_isLeft, Vector3? p_direction = null)
    {
        direction = p_direction.HasValue
            ? direction = p_direction.Value.normalized
            : new Vector3(p_isLeft ? -rawDirection.x : rawDirection.x, rawDirection.y, rawDirection.z).normalized;
    }

    public void LerpMove(float p_duration, float p_startSpeed) => StartCoroutine(LerpMoveCo(p_duration, p_startSpeed));
    public void ConstantMove(float p_duration, float p_startSpeed) => StartCoroutine(ConstantMoveCo(p_duration, p_startSpeed));

    private IEnumerator LerpMoveCo(float p_duration, float p_startSpeed)
    {
        var t_timer = 0f;
        var t_speed = boostFlag ? p_startSpeed : 0f;
        while (t_timer < p_duration)
        {
            t_speed = boostFlag
                ? Mathf.Lerp(p_startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, p_startSpeed, t_timer / p_duration);
            transform.position += direction * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ConstantMoveCo(float p_duration, float p_startSpeed)
    {
        var t_timer = 0f;
        
        while (t_timer < p_duration)
        {
            transform.position += direction * p_startSpeed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    } 
}

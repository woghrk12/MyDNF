using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public IEnumerator LerpMove(CharacterTransform p_transform, float p_duration, float p_startSpeed, Vector3 p_direction, bool p_boostFlag) 
        => LerpMoveCo(p_transform, p_duration, p_startSpeed, p_direction, p_boostFlag);
    public IEnumerator ConstantMove(CharacterTransform p_transform, float p_duration, float p_startSpeed, Vector3 p_direction) 
        => ConstantMoveCo(p_transform, p_duration, p_startSpeed, p_direction);

    private IEnumerator LerpMoveCo(CharacterTransform p_transform, float p_duration, float p_startSpeed, Vector3 p_direction, bool p_boostFlag)
    {
        var t_timer = 0f;
        var t_speed = 0f;
        var t_direction = p_direction.normalized;
        while (t_timer < p_duration)
        {
            t_speed = p_boostFlag
                ? Mathf.Lerp(p_startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, p_startSpeed, t_timer / p_duration);
            p_transform.Position += t_direction * t_speed * Time.deltaTime;

            if (p_transform.YPos < 0f) break;

            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ConstantMoveCo(CharacterTransform p_transform, float p_duration, float p_startSpeed, Vector3 p_direction)
    {
        var t_timer = 0f;
        var t_direction = p_direction.normalized;
        while (t_timer < p_duration)
        {
            p_transform.Position += t_direction * p_startSpeed * Time.deltaTime;

            if (p_transform.YPos < 0f) break;

            t_timer += Time.deltaTime;
            yield return null;
        }
    }
}

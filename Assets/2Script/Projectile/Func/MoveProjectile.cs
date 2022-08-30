using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public IEnumerator LerpMove(HitBox p_hitBox, float p_duration, float p_startSpeed, Vector3 p_direction, bool p_boostFlag) 
        => LerpMoveCo(p_hitBox, p_duration, p_startSpeed, p_direction, p_boostFlag);
    public IEnumerator ConstantMove(HitBox p_hitBox, float p_duration, float p_startSpeed, Vector3 p_direction) 
        => ConstantMoveCo(p_hitBox, p_duration, p_startSpeed, p_direction);

    private IEnumerator LerpMoveCo(HitBox p_hitBox, float p_duration, float p_startSpeed, Vector3 p_direction, bool p_boostFlag)
    {
        var t_timer = 0f;
        var t_speed = p_boostFlag ? p_startSpeed : 0f;
        var t_direction = p_direction.normalized;
        while (t_timer < p_duration)
        {
            t_speed = p_boostFlag
                ? Mathf.Lerp(p_startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, p_startSpeed, t_timer / p_duration);
            p_hitBox.ObjectPos += p_direction * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ConstantMoveCo(HitBox p_hitBox, float p_duration, float p_startSpeed, Vector3 p_direction)
    {
        var t_timer = 0f;
        var t_direction = p_direction.normalized;
        Debug.Log(t_direction);
        while (t_timer < p_duration)
        {
            p_hitBox.ObjectPos += p_direction * p_startSpeed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    } 
}

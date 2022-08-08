using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : Projectile
{
    private Vector3 direction = Vector3.zero;

    [SerializeField] private float startSpeed = 0f;
    [SerializeField] private bool boostFlag = false;
    protected override IEnumerator MoveProjectile(bool p_isLeft, float p_duration)
    {
        direction = p_isLeft ? Vector3.left : Vector3.right;

        var t_timer = 0f;
        var t_speed = boostFlag ? startSpeed : 0f;

        while (t_timer < p_duration)
        {
            t_speed = boostFlag ? Mathf.Lerp(startSpeed, 0f, t_timer / p_duration) : Mathf.Lerp(0f, startSpeed, t_timer / p_duration);
            transform.position += direction * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }
}

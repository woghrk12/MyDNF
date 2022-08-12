using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillA : Projectile
{
    [SerializeField] private float damageInterval = 0f;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        yield return ActivateProjectile(duration);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_duration)
    {
        StartCoroutine(CheckOnHit(duration));
        StartCoroutine(MoveProjectile(duration));

        yield return new WaitForSeconds(p_duration);
    }

    protected override IEnumerator CheckOnHit(float p_duration)
    {
        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            hitBox.CalculateHitBox();
            CalculateOnHitEnemy();
            t_timer += damageInterval;
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void CalculateOnHitEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (hitBox.CalculateOnHit(enemies[i]))
            {
                Debug.Log("Hit");
            }
        }
    }
}

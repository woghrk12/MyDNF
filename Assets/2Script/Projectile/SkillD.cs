using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillD : Projectile
{
    [SerializeField] private float canExplosion = 0f;
    [SerializeField] private string explosion = null;
    private bool explosionFlag = false;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        yield return ActivateProjectile(duration);
        explosionFlag = CheckExplosion(p_sizeEff);
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
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CalculateOnHitEnemy()
    {
        var t_enemies = enemies.ToList();

        for (int i = 0; i < t_enemies.Count; i++)
        {
            if (hitBox.CalculateOnHit(t_enemies[i]))
            {
                enemies.Remove(t_enemies[i]);
                Debug.Log("Hit");
            }
        }
    }

    private bool CheckExplosion(float p_value) { return p_value >= canExplosion; }

    protected override void EndProjectile()
    {
        if (explosionFlag)
        {
            var t_explosion = ObjectPoolingManager.SpawnObject(explosion, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            t_explosion.Shot(transform.position);
        }

        base.EndProjectile();
    }
}

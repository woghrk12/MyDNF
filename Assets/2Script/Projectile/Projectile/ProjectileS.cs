using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileS : Projectile
{
    [SerializeField] private ContinuousHit hitController = null;
    [SerializeField] private string explosion = null;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        StartCoroutine(AdditionalControl(p_button, duration));
        yield return ActivateProjectile();
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_timesValue = 1f)
    {
        hitController.StartCheckOnHit(coefficient, duration, hitBox, targets);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator AdditionalControl(string p_button, float p_duration)
    {
        yield return new WaitForSeconds(0.5f);

        var t_timer = 0f;
        var t_duration = duration - 0.5f;
        while (t_timer < t_duration)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN)
            {
                hitController.StopCheckOnHit();

                var t_explosion = ObjectPoolingManager.SpawnObject(explosion, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
                t_explosion.Shot(transform.position);
                
                EndProjectile();
                
                yield break;
            }

            t_timer += Time.deltaTime;
            yield return null;
        }
        
    }
}

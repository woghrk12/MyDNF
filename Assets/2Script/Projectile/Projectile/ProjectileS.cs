using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileS : Projectile
{
    [SerializeField] private ContinuousHit hitController = null;
    [SerializeField] private string explosion = null;
    private Coroutine runningCo = null;

    protected override IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff)
    {
        SetProjectile(p_position, p_isLeft, p_sizeEff);
        StartProjectile();
        StartCoroutine(AdditionalControl(p_button, duration));
        yield return ActivateProjectile(duration);
        EndProjectile();
    }

    protected override IEnumerator ActivateProjectile(float p_duration)
    {
        runningCo = StartCoroutine(hitController.CheckOnHit(duration, hitBox, enemies));

        yield return new WaitForSeconds(p_duration);
    }

    private IEnumerator AdditionalControl(string p_button, float p_duration)
    {
        var t_timer = 0f;
        while (t_timer < duration)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN)
            {
                StopCoroutine(runningCo);

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

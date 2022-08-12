using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousHit : MonoBehaviour
{
    [SerializeField] private float damageInterval = 0f;

    public IEnumerator CheckOnHit(float p_duration, HitBox p_hitBox, List<HitBox> p_targets)
    {
        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            p_hitBox.CalculateHitBox();
            CalculateOnHitEnemy(p_hitBox, p_targets);
            t_timer += damageInterval;
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void CalculateOnHitEnemy(HitBox p_hitBox, List<HitBox> p_targets)
    {
        for (int i = 0; i < p_targets.Count; i++)
        {
            if (p_hitBox.CalculateOnHit(p_targets[i]))
            {
                Debug.Log("Hit");
            }
        }
    }
}

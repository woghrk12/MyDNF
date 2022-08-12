using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstanceHit : MonoBehaviour
{
    public IEnumerator CheckOnHit(float p_duration, HitBox p_hitBox, List<HitBox> p_targets)
    {
        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            p_hitBox.CalculateHitBox();
            CalculateOnHitEnemy(p_hitBox, p_targets);
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CalculateOnHitEnemy(HitBox p_hitBox, List<HitBox> p_targets)
    {
        var t_enemies = p_targets.ToList();

        for (int i = 0; i < t_enemies.Count; i++)
        {
            if (p_hitBox.CalculateOnHit(t_enemies[i]))
            {
                p_targets.Remove(t_enemies[i]);
                Debug.Log("Hit");
            }
        }
    }
}

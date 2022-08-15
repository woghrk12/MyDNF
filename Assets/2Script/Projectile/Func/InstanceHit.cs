using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstanceHit : MonoBehaviour
{
    public IEnumerator CheckOnHit(int p_coEff, float p_duration, HitBox p_hitBox, List<HitBox> p_targets)
    {
        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            p_hitBox.CalculateHitBox();
            CalculateOnHitEnemy(p_hitBox, p_targets, p_coEff);
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CalculateOnHitEnemy(HitBox p_hitBox, List<HitBox> p_targets, int p_coEff)
    {
        var t_targets = p_targets.ToList();

        for (int i = 0; i < t_targets.Count; i++)
        {
            if (p_hitBox.CalculateOnHit(t_targets[i]))
            {
                p_targets.Remove(t_targets[i]);
                
                if(t_targets[i].OnDamageEvent != null)
                    t_targets[i].OnDamageEvent.Invoke(p_coEff);
            }
        }
    }
}

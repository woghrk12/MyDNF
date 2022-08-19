using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousHit : MonoBehaviour
{
    [SerializeField] private float damageInterval = 0f;
    [SerializeField] private bool isKnockBack = true;
    [SerializeField] private float knockBackPower = 0f;
    [SerializeField] private float hitStunTime = 0f;

    public IEnumerator CheckOnHit(int p_coEff, float p_duration, Transform p_posObj, Transform p_yPosObj, HitBox p_hitBox, List<HitBox> p_targets)
    {
        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            p_hitBox.CalculateHitBox(p_posObj, p_yPosObj);
            CalculateOnHitEnemy(p_hitBox, p_targets, p_coEff);
            t_timer += damageInterval;
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void CalculateOnHitEnemy(HitBox p_hitBox, List<HitBox> p_targets, int p_coEff)
    {
        for (int i = 0; i < p_targets.Count; i++)
        {
            if (p_hitBox.CalculateOnHit(p_targets[i]))
            {
                if (p_targets[i].OnDamageEvent != null)
                    p_targets[i].OnDamageEvent.Invoke(
                        p_coEff,
                        isKnockBack
                        ? (p_targets[i].transform.position - transform.position).normalized
                        : (transform.position - p_targets[i].transform.position).normalized,
                        hitStunTime,
                        knockBackPower
                        );
            }
        }
    }
}

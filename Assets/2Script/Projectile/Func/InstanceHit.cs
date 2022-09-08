using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class InstanceHit : MonoBehaviour
{
    [SerializeField] private bool isKnockBack = true;
    [SerializeField] private float knockBackPower = 0f;
    [SerializeField] private float hitStunTime = 0f;
    [SerializeField] private bool isPiercing = true;

    private Coroutine runningCo = null;
    private UnityAction hitEvent = null;
    public UnityAction HitEvent { set { hitEvent = value; } get { return hitEvent; } }

    public void StartCheckOnHit(int p_coEff, float p_duration, HitBox p_hitBox, List<HitBox> p_targets)
       => runningCo = StartCoroutine(CheckOnHit(p_coEff, p_duration, p_hitBox, p_targets));
    public void StopCheckOnHit()
        => StopCoroutine(runningCo);

    private IEnumerator CheckOnHit(int p_coEff, float p_duration, HitBox p_hitBox, List<HitBox> p_targets)
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
            if (!t_targets[i].enabled) continue;
            if (p_hitBox.CalculateOnHit(t_targets[i]))
            {
                var t_hitEffect = ObjectPoolingManager.SpawnObject("HitEffect", Vector3.zero, Quaternion.identity).GetComponent<HitEffect>();
                t_hitEffect.SetPosition(p_hitBox, t_targets[i]);
                t_hitEffect.StartHitEffect();
                p_targets.Remove(t_targets[i]);
                if (hitEvent != null) hitEvent.Invoke();
                if (t_targets[i].OnDamageEvent != null)
                    t_targets[i].OnDamageEvent.Invoke(
                        p_coEff,
                        isKnockBack
                        ? new Vector3(t_targets[i].XPos - p_hitBox.XPos, 0f, 0f).normalized
                        : new Vector3(p_hitBox.XPos - t_targets[i].XPos, 0f, 0f).normalized,
                        hitStunTime,
                        knockBackPower
                        );
                if (!isPiercing) { StopCheckOnHit(); gameObject.SetActive(false); break; }
            }
        }
    }
}

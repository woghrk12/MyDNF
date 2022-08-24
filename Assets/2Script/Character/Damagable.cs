using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private Coroutine onDamageCo = null;
    
    public bool OnDamage(Status p_hitTarget, int p_damage, Vector3 p_dir, float p_hitStunTime, float p_knockBackPower)
    {
        p_hitTarget.CurHealth -= p_damage;

        if (onDamageCo != null) StopCoroutine(onDamageCo);

        if (p_hitTarget.CurHealth <= 0) 
        {
            OnDie();
            return false;
        }

        onDamageCo = StartCoroutine(KnockBackEffect(p_dir, p_hitStunTime, p_knockBackPower));
        return true;
    }

    private IEnumerator KnockBackEffect(Vector3 p_dir, float p_hitStunTime, float p_knockBackPower)
    {
        anim.SetBool("isEndHit", false);
        anim.SetTrigger("OnHit");

        var t_timer = 0f;
        var t_knockBackPower = p_knockBackPower;
        transform.localScale = new Vector3(p_dir.x < 0f ? 1f : -1f, 1f, 1f);

        while (t_timer < p_hitStunTime)
        {
            t_knockBackPower = Mathf.Lerp(p_knockBackPower, 0f, t_timer / p_hitStunTime);
            transform.position += p_dir * t_knockBackPower * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("isEndHit", true);
    }

    private void OnDie()
    {
        anim.SetTrigger("Die");
    }
}

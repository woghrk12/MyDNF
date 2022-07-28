using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private float attackSpeed = 1f;
    public float AttackSpeed { set { attackSpeed = value < 1f ? 1f : value; } get { return attackSpeed; } }

    private Coroutine attackCo = null;
    
    private int numClicks = 0;
    [SerializeField] private float maxComboDelay = 0f;

    private bool isAttack = false;

    [SerializeField] private Projectile baseAttack = null;

    public IEnumerator Attack() => AttackCo();
    public IEnumerator GetInput() => GetInputCo();
    private IEnumerator GetInputCo()
    {
        isAttack = true;

        var t_timer = 0f;
        while (t_timer <= maxComboDelay)
        {
            if (Input.GetButtonDown("Attack")) numClicks++;
            if (!isAttack) break;
            t_timer += Time.deltaTime;
            yield return null;
        }

        numClicks = 0;
    }
    
    private IEnumerator AttackCo()
    {
        yield return AttackOne();

        if (numClicks >= 2)
            yield return AttackTwo();

        if (numClicks >= 3)
            yield return AttackThree();

        attackCo = null;
        isAttack = false;
    }
    
    private IEnumerator AttackOne()
    {
        anim.SetTrigger("Attack");
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack", transform.position, Quaternion.identity).GetComponent<Projectile>();
        t_effect.DestroyProjectile();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator AttackTwo()
    {
        anim.SetBool("isAttackTwo", true);
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack", transform.position, Quaternion.identity).GetComponent<Projectile>();
        t_effect.DestroyProjectile();
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isAttackTwo", false);
    }

    private IEnumerator AttackThree()
    {
        anim.SetBool("isAttackThree", true);
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack", transform.position, Quaternion.identity).GetComponent<Projectile>();
        t_effect.DestroyProjectile();
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttackThree", false);
    }
}

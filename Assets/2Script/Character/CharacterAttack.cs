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

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && !isAttack)
            StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        isAttack = true;

        attackCo = StartCoroutine(Attack());
        
        var t_timer = 0f;
        while (t_timer <= maxComboDelay)
        {
            if (Input.GetButtonDown("Attack")) numClicks++;

            t_timer += Time.deltaTime;
            yield return null;
        }   

        isAttack = false;
        numClicks = 0;
    }
    
    private IEnumerator Attack()
    {
        yield return AttackOne();

        if (numClicks >= 2)
            yield return AttackTwo();

        if (numClicks >= 3)
            yield return AttackThree();

        attackCo = null;
    }
    
    private IEnumerator AttackOne()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator AttackTwo()
    {
        anim.SetBool("isAttackTwo", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isAttackTwo", false);
    }

    private IEnumerator AttackThree()
    {
        anim.SetBool("isAttackThree", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isAttackThree", false);
    }
}

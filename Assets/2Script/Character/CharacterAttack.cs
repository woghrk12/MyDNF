using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private float attackSpeed = 1f;
    public float AttackSpeed { set { attackSpeed = value < 1f ? 1f : value; } get { return attackSpeed; } }
    
    private int numClicks = 0;
    [SerializeField] private float maxComboDelay = 0f;

    private bool isAttack = false;

    [SerializeField] private Skill skillA = null;

    public IEnumerator Attack() => AttackCo();
    public IEnumerator InputCombo(string p_buttonName) => InputComboCo(p_buttonName);
    public bool CanUseSkill() { return skillA.CanUse; }
    public IEnumerator SkillA(float p_delay, bool p_isLeft) => SkillACo(p_delay, p_isLeft);

    private IEnumerator InputComboCo(string p_buttonName)
    {
        isAttack = true;

        var t_timer = 0f;
        while (t_timer <= maxComboDelay)
        {
            if (Input.GetButtonDown(p_buttonName)) numClicks++;
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

        isAttack = false;
    }
    
    private IEnumerator AttackOne()
    {
        anim.SetTrigger("Attack");
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack1", transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator AttackTwo()
    {
        anim.SetBool("isAttackTwo", true);
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack2", transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isAttackTwo", false);
    }

    private IEnumerator AttackThree()
    {
        anim.SetBool("isAttackThree", true);
        var t_effect = ObjectPoolingManager.SpawnObject("BaseAttack3", transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttackThree", false);
    }

    private IEnumerator SkillACo(float p_delay, bool p_isLeft)
    {
        if (!skillA.CanUse)
        {
            Debug.Log("Can't use Skill");
            yield break;
        }
        skillA.UseSkill(p_isLeft);
        anim.SetTrigger(skillA.SkillMotion);

        yield return new WaitForSeconds(p_delay);
    }
}

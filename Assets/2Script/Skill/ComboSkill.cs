using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : Skill
{
    [SerializeField] private int totalNumCombo = 0;
    [SerializeField] private float maxComboTime = 0f;
    public float MaxComboTime { get { return maxComboTime; } }
    private int numOfClicks = 0;
    public int NumOfClicks { set { numOfClicks = value; } get { return numOfClicks; } }

    private bool isAttack = false;
    public bool IsAttack { set { isAttack = value; } get { return isAttack; } }

    public override IEnumerator UseSkill(bool p_isLeft)
    {
        waitingTime = coolTime;

        var t_cnt = 0;
        while (t_cnt < totalNumCombo)
        {
            StartCoroutine(OnEffect(skillEffect[t_cnt], transform.position, p_isLeft, duration[t_cnt]));
            yield return CheckOnHit(hitBox[t_cnt], enemies, p_isLeft, duration[t_cnt]);

            t_cnt++;

            if (numOfClicks <= t_cnt)
                break;
        }

        IsAttack = false;
    }
}

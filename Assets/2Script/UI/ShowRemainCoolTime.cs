using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRemainCoolTime : MonoBehaviour
{
    private Skill skill = null;
    public Skill Skill { set { skill = value; } get { return skill; } }
    [SerializeField] private Text remainCoolTimeText = null;
    [SerializeField] private Image buttonImage = null;
    private bool canUse = false;

    private void Update()
    {
        ShowSkillCoolTime();
    }

    public void SetSkill(Skill p_skill)
    {
        skill = p_skill;
    }

    private void ShowSkillCoolTime()
    {
        var t_color = buttonImage.color;
        
        if (skill == null)
        {
            remainCoolTimeText.enabled = false;
            t_color.a = 0.1f;
            buttonImage.color = t_color;
            return;
        }

        if (!skill.CanUse)
        {
            remainCoolTimeText.enabled = true;
            remainCoolTimeText.text = skill.WaitingTime.ToString();

            t_color.a = 0.5f;
            buttonImage.color = t_color;
            return;
        }

        remainCoolTimeText.enabled = false;
        t_color.a = 1f;
        buttonImage.color = t_color;

    }
}

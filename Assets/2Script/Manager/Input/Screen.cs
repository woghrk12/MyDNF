using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private SkillManager skillManager = null;

    [SerializeField] private Joystick joystick = null;

    [SerializeField] private ShowRemainCoolTime aButton = null;
    public ShowRemainCoolTime AButton { get { return aButton; } }
    [SerializeField] private ShowRemainCoolTime sButton = null;
    public ShowRemainCoolTime SButton { get { return sButton; } }
    [SerializeField] private ShowRemainCoolTime dButton = null;
    public ShowRemainCoolTime DButton { get { return dButton; } }
    [SerializeField] private ShowRemainCoolTime fButton = null;
    public ShowRemainCoolTime FButton { get { return fButton; } }

    [SerializeField] private ControlSlider healthSlider = null;
    [SerializeField] private ControlSlider manaSlider = null;

    [SerializeField] private Status playerStatus = null;

    private void OnEnable()
    {
        playerStatus.SetControlSlider(healthSlider, manaSlider);
        aButton.Skill = skillManager.ASkill;
        sButton.Skill = skillManager.SSkill;
        dButton.Skill = skillManager.DSkill;
        fButton.Skill = skillManager.FSkill;
    }

    private void Update()
    {
        inputManager.Direction = joystick.Direction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private SkillManager skillManager = null;

    [SerializeField] private ShowRemainCoolTime aButton = null;
    public ShowRemainCoolTime AButton { get { return aButton; } }
    [SerializeField] private ShowRemainCoolTime sButton = null;
    public ShowRemainCoolTime SButton { get { return sButton; } }
    [SerializeField] private ShowRemainCoolTime dButton = null;
    public ShowRemainCoolTime DButton { get { return dButton; } }
    [SerializeField] private ShowRemainCoolTime fButton = null;
    public ShowRemainCoolTime FButton { get { return fButton; } }

    private float hAxis = 0f;
    private float vAxis = 0f;

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
        GetInput();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
        inputManager.Direction = Vector3.ClampMagnitude(new Vector3(hAxis, 0f, vAxis), 1f);
    }
}

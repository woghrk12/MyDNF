using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private CharacterMove moveController = null;
    [SerializeField] private CharacterJump jumpController = null;
    [SerializeField] private CharacterAttack attackController = null;
    [SerializeField] private SkillManager skillManager = null;

    private bool IsLeft { get { return moveController.IsLeft; } }

    public bool CanMove { set { moveController.CanMove = value; } get { return moveController.CanMove; } }
    private bool canJump = true;
    private bool canAttack = true;

    [SerializeField] private string xButton = "X";
    [SerializeField] private string aButton = "A";
    [SerializeField] private string jumpButton = "Jump";

    private void Update()
    {
        if (Input.GetButtonDown(xButton) && canAttack)
            UseSkill(skillManager.BaseAttack, xButton);
        if (Input.GetButtonDown(aButton) && canAttack)
            UseSkill(skillManager.ASkill, aButton);
        if (Input.GetButtonDown(jumpButton) && canJump)
            StartCoroutine(Jump());
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;

        Move();
    }

    private void Move() => moveController.Move();

    private IEnumerator Jump()
    {
        canJump = false;
        canAttack = false;
        
        yield return jumpController.Jump();
        
        canAttack = true; 
        canJump = true;
    }

    private void UseSkill(Skill p_skill, string p_button)
    {
        switch (p_skill.skillType)
        {
            case ESkillType.SINGLE:
                StartCoroutine(UseSkillCo(p_skill));
                break;

            case ESkillType.COMBO:
                StartCoroutine(UseSkillCo(p_skill.GetComponent<ComboSkill>(), p_button));
                break;
        }
    }

    private IEnumerator UseSkillCo(Skill p_skill)
    {
        if (!p_skill.CanUse) yield break;

        canAttack = false;
        canJump = false;
        CanMove = false;

        yield return attackController.UseSkill(p_skill, IsLeft);

        CanMove = true;
        canJump = true;
        canAttack = true;
    }

    private IEnumerator UseSkillCo(ComboSkill p_skill, string p_button)
    {
        if (!p_skill.CanUse) yield break;

        canAttack = false;
        canJump = false;
        CanMove = false;

        StartCoroutine(attackController.InputCombo(p_skill, p_button));
        yield return attackController.UseSkill(p_skill, IsLeft);

        CanMove = true;
        canJump = true;
        canAttack = true;
    }
}

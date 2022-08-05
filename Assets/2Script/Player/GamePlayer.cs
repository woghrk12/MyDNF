using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private InputManager inputController = null;
    [SerializeField] private CharacterMove moveController = null;
    [SerializeField] private CharacterJump jumpController = null;
    [SerializeField] private CharacterAttack attackController = null;
    [SerializeField] private SkillManager skillManager = null;

    private bool IsLeft { get { return moveController.IsLeft; } }

    private bool CanMove { set { moveController.CanMove = value; } get { return moveController.CanMove; } }
    private bool canJump = true;
    private bool canAttack = true;

    private Coroutine runningCo = null;

    private void Update()
    {
        if (inputController.GetButtonDown(inputController.XButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.BaseAttack, inputController.XButton));
        if (inputController.GetButtonDown(inputController.AButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.ASkill, inputController.AButton));
        if (inputController.GetButtonDown(inputController.SButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.SSkill, inputController.SButton));
        if (inputController.GetButtonDown(inputController.DButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.DSkill, inputController.DButton));
        if (inputController.GetButtonDown(inputController.JButton) && canJump)
            StartCoroutine(Jump());
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;

        Move();
    }

    private void Move() => moveController.Move(inputController.Direction);
    
    private IEnumerator Jump()
    {
        canJump = false;
        canAttack = false;
        
        yield return jumpController.Jump();
        
        canAttack = true; 
        canJump = true;
    }

    private IEnumerator CheckCanUseSkill(Skill p_skill, string p_button)
    {
        if (!p_skill.CanUse) yield break;

        canJump = false;
        CanMove = false;

        if (runningCo != null)
        {
            if (!skillManager.CheckCanCancel(attackController.runningSkill, p_skill)) yield break;

            StopCoroutine(runningCo);
            attackController.CancelSkill(attackController.runningSkill);
            yield return null;
        }

        runningCo = StartCoroutine(UseSkill(p_skill, p_button));
    }

    private IEnumerator UseSkill(Skill p_skill, string p_button)
    {
        yield return attackController.UseSkill(p_skill, IsLeft, p_button);

        CanMove = true;
        canJump = true;

        runningCo = null;
    }
}

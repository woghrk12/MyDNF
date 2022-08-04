using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private KeyboardManager inputController = null;
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
        if (inputController.XDown && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.BaseAttack, inputController.XDown));
        if (inputController.ADown && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.ASkill, inputController.ADown));
        if (inputController.SDown && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.SSkill, inputController.SDown));
        if (inputController.JDown && canJump)
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

    private IEnumerator CheckCanUseSkill(Skill p_skill, bool p_button)
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

    private IEnumerator UseSkill(Skill p_skill, bool p_button)
    {
        yield return attackController.UseSkill(p_skill, IsLeft, p_button);

        CanMove = true;
        canJump = true;

        runningCo = null;
    }
}

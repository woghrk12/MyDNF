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

    private bool CanMove { set { moveController.CanMove = value; } get { return moveController.CanMove; } }
    private bool canJump = true;
    private bool canAttack = true;

    [SerializeField] private KeyCode xButton = KeyCode.X;
    [SerializeField] private KeyCode aButton = KeyCode.A;
    [SerializeField] private KeyCode sButton = KeyCode.S;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(xButton) && canAttack)
            UseSkill(skillManager.BaseAttack, xButton);
        if (Input.GetKeyDown(aButton) && canAttack)
            StartCoroutine(UseSkill(skillManager.ASkill, aButton));
        if (Input.GetKeyDown(sButton) && canAttack)
            StartCoroutine(UseSkill(skillManager.SSkill, sButton));
        if (Input.GetKeyDown(jumpButton) && canJump)
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

    private IEnumerator UseSkill(Skill p_skill, KeyCode p_button)
    {
        if (!p_skill.CanUse) yield break;

        canAttack = false;
        canJump = false;
        CanMove = false;

        yield return attackController.UseSkill(p_skill, IsLeft, p_button);

        CanMove = true;
        canJump = true;
        canAttack = true;
    }

}

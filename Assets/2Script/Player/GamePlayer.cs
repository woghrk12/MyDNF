using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private CharacterMove moveController = null;
    [SerializeField] private CharacterJump jumpController = null;
    [SerializeField] private CharacterAttack attackController = null;

    private bool IsLeft { get { return moveController.IsLeft; } }

    public bool CanMove { set { moveController.CanMove = value; } get { return moveController.CanMove; } }
    private bool canJump = true;
    private bool canAttack = true;

    [SerializeField] private float skillADelay = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && canAttack)
            StartCoroutine(Attack());
        if (Input.GetButtonDown("SkillA") && canAttack)
            StartCoroutine(SkillA(skillADelay));
        if (Input.GetButtonDown("Jump") && canJump)
            StartCoroutine(Jump());
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;

        Move();
    }

    private void Move() => moveController.Move();

    private IEnumerator Attack()
    {
        canAttack = false;
        canJump = false;
        CanMove = false;

        StartCoroutine(attackController.GetInput());
        yield return attackController.Attack();

        CanMove = true;
        canJump = true;
        canAttack = true;
    }

    private IEnumerator Jump()
    {
        canJump = false;
        canAttack = false;
        
        yield return jumpController.Jump();
        
        canAttack = true; 
        canJump = true;
    }

    private IEnumerator SkillA(float p_delay)
    {
        canAttack = false;
        canJump = false;
        CanMove = false;

        yield return attackController.SkillA(p_delay, IsLeft);

        CanMove = true;
        canJump = true;
        canAttack = true;
    }
}

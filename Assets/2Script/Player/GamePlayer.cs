using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private InputManager inputController = null;
    [SerializeField] private CharacterMove moveController = null;
    [SerializeField] private CharacterJump jumpController = null;
    [SerializeField] private CharacterAttack attackController = null;
    [SerializeField] private CharacterTransform transformController = null;
    [SerializeField] private Damagable healthController = null;
    [SerializeField] private SkillManager skillManager = null;
    [SerializeField] private Status statusManager = null;

    private RoomManager roomManager = null;

    private bool IsLeft { set { moveController.IsLeft = value; } get { return moveController.IsLeft; } }
    private bool isDie = false;
    public bool IsDie 
    { 
        private set
        {
            isDie = value;
            hitBox.enabled = !isDie;
            CanMove = !isDie;
            canJump = !isDie;
            canAttack = !isDie;
            roomManager.IsEndGame = true;
        }
        get { return isDie; } 
    }


    private bool CanMove { set { moveController.CanMove = value; } get { return moveController.CanMove; } }
    private bool canJump = true;
    private bool canAttack = true;

    private Coroutine runningCo = null;

    private void Awake()
    {
        statusManager.InitializeValue();                    
        jumpController.InitializeValue(hitBox);

        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    private void OnEnable()
    {
        hitBox.OnDamageEvent += OnDamage;
    }

    private void OnDisable()
    {
        hitBox.OnDamageEvent -= OnDamage;
    }

    private void Update()
    {
        hitBox.CalculateHitBox();

        if (inputController.GetButtonDown(inputController.XButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.BaseAttack, inputController.XButton));
        if (inputController.GetButtonDown(inputController.AButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.ASkill, inputController.AButton));
        if (inputController.GetButtonDown(inputController.SButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.SSkill, inputController.SButton));
        if (inputController.GetButtonDown(inputController.DButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.DSkill, inputController.DButton));
        if (inputController.GetButtonDown(inputController.FButton) && canAttack)
            StartCoroutine(CheckCanUseSkill(skillManager.FSkill, inputController.FButton));
        if (inputController.GetButtonDown(inputController.JButton) && canJump)
            StartCoroutine(Jump());
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;

        Move();
    }

    private void Move() => moveController.Move(transformController, inputController.Direction);
    
    private IEnumerator Jump()
    {
        canJump = false;
        canAttack = false;
        
        yield return jumpController.Jump(transformController);
        
        canAttack = true; 
        canJump = true;
    }

    private IEnumerator CheckCanUseSkill(Skill p_skill, string p_button)
    {
        if (!p_skill.CanUse) yield break;
        if (!statusManager.CheckMana(p_skill.NeedMana)) yield break;
        if (!CheckCancelSkill(p_skill)) yield break;

        yield return null;

        runningCo = StartCoroutine(UseSkill(p_skill, p_button));
    }

    private bool CheckCancelSkill(Skill p_skill)
    {
        if (attackController.RunningSkill == null) return true;
        if (!skillManager.CheckCanCancel(attackController.RunningSkill, p_skill)) return false;
        
        if (p_skill.CanUseWithoutCancel)
        {
            statusManager.UseMana(p_skill.NeedMana);
            attackController.UseSkillWithoutCancel(p_skill, IsLeft);
            return false;
        }

        StopCoroutine(runningCo);
        attackController.CancelSkill(attackController.RunningSkill);
        return true;
    }

    private IEnumerator UseSkill(Skill p_skill, string p_button)
    {
        statusManager.UseMana(p_skill.NeedMana);

        if (!p_skill.CanUseWithoutCancel)
        {
            canJump = false;
            CanMove = false;
        }

        yield return attackController.UseSkill(p_skill, IsLeft, p_button, p_skill.CanUseWithoutCancel);

        CanMove = true;
        canJump = true;

        runningCo = null;
    }

    private void OnDamage(int p_damage, Vector3 p_dir, float p_hitStunTime, float p_knockBackPower)
    {
        if (runningCo != null) StopCoroutine(runningCo);
        if (attackController.RunningSkill != null) attackController.CancelSkill(attackController.RunningSkill);

        CanMove = false;
        canJump = false;
        canAttack = false;

        IsLeft = p_dir.x > 0f;

        if (healthController.OnDamage(statusManager, transformController, p_damage, p_dir, p_hitStunTime, p_knockBackPower))
            runningCo = StartCoroutine(OnDamageCo(p_hitStunTime));
        else IsDie = true;
    }

    private IEnumerator OnDamageCo(float p_hitStunTime)
    {
        yield return new WaitForSeconds(p_hitStunTime);

        CanMove = true;
        canJump = true;
        canAttack = true;
        runningCo = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private Damagable healthController = null;
    [SerializeField] private EnemyAttack attackController = null;
    [SerializeField] private EnemyMove moveController = null;

    private RoomManager roomManager = null;
    private GamePlayer player = null;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        player = roomManager.Player;
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
    }

    private void OnDamage(int p_damage, Vector3 p_dir, float p_hitStunTime, float p_knockBackPower)
    {
        attackController.CancelAttack(anim);
        healthController.OnDamage(p_damage, p_dir, p_hitStunTime, p_knockBackPower);
    }
}

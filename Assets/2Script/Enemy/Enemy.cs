using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private Transform yPosObject = null;

    [SerializeField] private Damagable healthController = null;
    [SerializeField] private EnemyAttack attackController = null;
    [SerializeField] private EnemyMove moveController = null;

    private RoomManager roomManager = null;
    private GamePlayer player = null;

    [SerializeField] private EnemyPattern[] patterns = null;

    public bool IsLeft { get { return player.transform.position.x < transform.position.x; } }

    private Coroutine runningCo = null;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        player = roomManager.Player;

        hitBox.OnDamageEvent += OnDamage;
    }

    private void Start()
    {
        runningCo = StartCoroutine(StartPattern());
    }

    private void Update()
    {
        hitBox.CalculateHitBox(transform.position, yPosObject.localPosition);
    }

    private IEnumerator StartPattern()
    {
        var t_pattern = ChoosePattern(patterns);
        yield return new WaitForSeconds(t_pattern.coolTime + Random.Range(0f, 2f));

        switch (t_pattern.patternType)
        {
            case EEnemyPatternType.IDLE:
                break;

            case EEnemyPatternType.WALK:
                yield return moveController.MovePattern(player.transform, t_pattern.duration);

                break;
            case EEnemyPatternType.BASEATTACK:
            case EEnemyPatternType.SKILL:
                yield return attackController.AttackPattern(anim, t_pattern.patternType);
                break;
        }

        runningCo = StartCoroutine(StartPattern());
    }

    private EnemyPattern ChoosePattern(EnemyPattern[] p_patterns)
    {
        var t_pattern = new List<EnemyPattern>();

        for (int i = 0; i < p_patterns.Length; i++)
        {
            if (p_patterns[i].range == null)
            {
                t_pattern.Add(p_patterns[i]);
                continue;
            }

            p_patterns[i].range.SetDirection(IsLeft);
            p_patterns[i].range.CalculateHitBox(transform.position);
            if (p_patterns[i].range.CalculateOnHit(player.gameObject.GetComponent<HitBox>())) t_pattern.Add(p_patterns[i]);
        }

        var t_total = 0f;

        for (int i = 0; i < t_pattern.Count; i++)
            t_total += t_pattern[i].probability;

        var t_random = Random.value * t_total;

        for (int i = 0; i < p_patterns.Length; i++)
        {
            if (t_random < p_patterns[i].probability) return t_pattern[i];
            else t_random -= p_patterns[i].probability;
        }

        return t_pattern[t_pattern.Count- 1];
    }

    private void OnDamage(int p_damage, Vector3 p_dir, float p_hitStunTime, float p_knockBackPower)
    {
        if (runningCo != null) StopCoroutine(runningCo);

        attackController.ResetValue(anim);
        healthController.OnDamage(p_damage, p_dir, p_hitStunTime, p_knockBackPower);
    }
}

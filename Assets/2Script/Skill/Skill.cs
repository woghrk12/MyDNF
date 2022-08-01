using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Skill : MonoBehaviour
{
    [SerializeField] private float coefficientValue = 0f;
    [SerializeField] private string skillEffect = null;
    [SerializeField] private string skillMotion = "";
    [SerializeField] private float duration = 0f;
    [SerializeField] private float coolTime = 0f;
    protected float waitingTime = 0f;

    public bool CanUse { get { return waitingTime <= 0f; } }
    public string SkillMotion { get { return skillMotion; } }

    [SerializeField] private HitBox hitBox = null;

    private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    private void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public void UseSkill(bool p_isLeft)
    {
        if (!CanUse) return;
        waitingTime = coolTime;

        var t_effect = ObjectPoolingManager.SpawnObject(skillEffect, transform.position, Quaternion.identity);
        t_effect.transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);

        hitBox.SetDirection(p_isLeft);
        enemies = roomManager.enemiesHitBox.ToList();

        StartCoroutine(InvokeSkillCo(t_effect));
    }

    private IEnumerator InvokeSkillCo(GameObject p_obj)
    {
        var t_timer = 0f;

        while (t_timer <= duration)
        {
            hitBox.CalculateHitBox();
            CheckOnHit();
            t_timer += Time.deltaTime;
            yield return null;
        }

        ObjectPoolingManager.ReturnObject(p_obj.gameObject);
    }

    private void CheckOnHit()
    {
        var t_enemies = enemies;

        for (int i = 0; i < t_enemies.Count; i++)
        {
            if (hitBox.maxHitBoxX < t_enemies[i].minHitBoxX || hitBox.minHitBoxX > t_enemies[i].maxHitBoxX) continue;
            if (hitBox.maxHitBoxZ < t_enemies[i].minHitBoxZ || hitBox.minHitBoxZ > t_enemies[i].maxHitBoxZ) continue;
            if (hitBox.maxHitBoxY < t_enemies[i].minHitBoxY || hitBox.minHitBoxY > t_enemies[i].maxHitBoxY) continue;

            enemies.Remove(t_enemies[i]);
            Debug.Log("hit");
        }
    }
}


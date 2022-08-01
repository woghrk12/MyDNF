using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Skill : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private string skillEffect = null;
    [SerializeField] private float duration = 0f;
    [SerializeField] private float coefficientValue = 0f;
    [SerializeField] private float delay = 0f;

    [SerializeField] private float coolTime = 0f;
    protected float waitingTime = 0f;

    public bool CanUse { get { return waitingTime <= 0f; } }
    public float Delay { get { return delay; } }

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

    public IEnumerator UseSkill(bool p_isLeft)
    {
        waitingTime = coolTime;

        StartCoroutine(OnEffect(p_isLeft));
        yield return CheckOnHit(p_isLeft);
    }

    private IEnumerator OnEffect(bool p_isLeft)
    {
        var t_effect = ObjectPoolingManager.SpawnObject(skillEffect, transform.position, Quaternion.identity);
        t_effect.transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);

        yield return new WaitForSeconds(duration);

        ObjectPoolingManager.ReturnObject(t_effect);
    }

    private IEnumerator CheckOnHit(bool p_isLeft)
    {
        hitBox.SetDirection(p_isLeft);
        enemies = roomManager.enemiesHitBox.ToList();

        var t_timer = 0f;

        while (t_timer <= duration)
        {
            hitBox.CalculateHitBox();
            CalculateOnHit();
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CalculateOnHit()
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


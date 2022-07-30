using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;
    
    [SerializeField] private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    [SerializeField] private float duration = 0f;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    public void InvokeSkill(bool p_isLeft)
    {
        enemies = roomManager.enemiesHitBox.ToList();
        transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);
        hitBox.SetDirection(p_isLeft);
        StartCoroutine(InvokeSkillCo());
    }

    private IEnumerator InvokeSkillCo()
    {
        var t_timer = 0f;

        while (t_timer <= duration)
        {
            CheckOnHit();
            t_timer += Time.deltaTime;
            yield return null;
        }

        ObjectPoolingManager.ReturnObject(this.gameObject);
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


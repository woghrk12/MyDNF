using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;

    [SerializeField] private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    private void OnEnable()
    {
        enemies = roomManager.enemiesHitBox;
    }

    private void Update()
    {
        CheckOnHit();
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
            Debug.Log("Hit");
        }
    }
}

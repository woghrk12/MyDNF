using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosive : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private HitBox hitBox = null;

    private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    public void StartExplosion(Vector3 p_pos)
    {
        transform.position = p_pos;

        anim.SetTrigger("Explosion");
        StartCoroutine(CheckOnHit(0.6f));
    }

    private IEnumerator CheckOnHit(float p_duration)
    {
        enemies = roomManager.enemiesHitBox.ToList();

        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            hitBox.CalculateHitBox();
            CalculateOnHit(hitBox, enemies);
            t_timer += Time.deltaTime;
            yield return null;
        }

        ObjectPoolingManager.ReturnObject(this.gameObject);
    }

    private void CalculateOnHit(HitBox p_hitBox, List<HitBox> p_enemies)
    {
        var t_enemies = p_enemies.ToList();

        for (int i = 0; i < t_enemies.Count; i++)
        {
            if (p_hitBox.maxHitBoxX < t_enemies[i].minHitBoxX || p_hitBox.minHitBoxX > t_enemies[i].maxHitBoxX) continue;
            if (p_hitBox.maxHitBoxZ < t_enemies[i].minHitBoxZ || p_hitBox.minHitBoxZ > t_enemies[i].maxHitBoxZ) continue;
            if (p_hitBox.maxHitBoxY < t_enemies[i].minHitBoxY || p_hitBox.minHitBoxY > t_enemies[i].maxHitBoxY) continue;

            p_enemies.Remove(t_enemies[i]);
            Debug.Log("hit");
        }
    }
}

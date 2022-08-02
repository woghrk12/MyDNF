using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private HitBox hitBox = null;

    [SerializeField] private float duration = 0f;

    private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    protected void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    public void StartProjectile(bool p_isLeft)
    {
        transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);
        StartCoroutine(CheckOnHit(p_isLeft, duration));
        StartCoroutine(MoveProjectile(p_isLeft, duration));
    }

    protected abstract IEnumerator MoveProjectile(bool p_isLeft, float p_duration);

    private IEnumerator CheckOnHit(bool p_isLeft, float p_duration)
    {
        anim.SetTrigger("Shot");
        hitBox.SetDirection(p_isLeft);
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

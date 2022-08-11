using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private HitBox hitBox = null;
    [SerializeField] private Transform scaleObject = null;
    [SerializeField] private float duration = 0f;

    private Vector3 direction = Vector3.zero;

    [SerializeField] private float startSpeed = 0f;
    [SerializeField] private bool boostFlag = false;

    private RoomManager roomManager = null;
    private List<HitBox> enemies = null;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    private void OnEnable()
    {
        scaleObject.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Shot(Vector3 p_position, Vector3 p_dir, bool p_isLeft = false, float p_sizeEff = 1f) 
        => StartCoroutine(ShotCo(p_position, p_dir, p_isLeft, p_sizeEff));

    private IEnumerator ShotCo(Vector3 p_position, Vector3 p_dir, bool p_isLeft, float p_sizeEff = 1f)
    {
        SetProjectile(p_position, p_dir, p_isLeft, p_sizeEff);
        ShotProjectile();

        StartCoroutine(CheckOnHitCo(duration));

        yield return new WaitForSeconds(duration);

        EndProjectile();
    }

    protected virtual void SetProjectile(Vector3 p_position, Vector3 p_dir, bool p_isLeft, float p_sizeEff = 1f)
    {
        transform.position = p_position;
        scaleObject.localScale = new Vector3(p_isLeft ? -p_sizeEff : p_sizeEff, p_sizeEff, 1f);
        hitBox.ScaleHitBox(p_sizeEff);
        hitBox.SetDirection(p_isLeft);

        direction = new Vector3(p_isLeft ? -p_dir.x : p_dir.x, p_dir.y, p_dir.z).normalized;
    }

    protected virtual void ShotProjectile()
    {
        anim.SetTrigger("Shot");
        StartCoroutine(MoveProjectile(direction, duration));
    }

    private IEnumerator MoveProjectile(Vector3 p_dir, float p_duration)
    {
        var t_timer = 0f;
        var t_speed = boostFlag ? startSpeed : 0f;

        while (t_timer < p_duration)
        {
            t_speed = boostFlag
                ? Mathf.Lerp(startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, startSpeed, t_timer / p_duration);
            transform.position += p_dir * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CheckOnHitCo(float p_duration)
    {
        enemies = roomManager.enemiesHitBox.ToList();

        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            hitBox.CalculateHitBox();
            CheckOnHit(enemies);
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CheckOnHit(List<HitBox> p_enemies)
    {
        var t_enemies = p_enemies.ToList();

        for (int i = 0; i < t_enemies.Count; i++)
        {
            if (hitBox.CalculateOnHit(t_enemies[i]))
            {
                p_enemies.Remove(t_enemies[i]);
                Debug.Log("Hit");
            }
        }
    }

    protected virtual void EndProjectile()
    {
        anim.SetTrigger("End");

        ObjectPoolingManager.ReturnObject(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Animator anim = null;
    [SerializeField] protected HitBox hitBox = null;
    [SerializeField] protected Transform scaleObject = null;
    [SerializeField] protected float duration = 0f;

    [SerializeField] private Vector3 direction = Vector3.zero;
    public Vector3 Direction { set { direction = value.normalized; } get { return direction; } }

    [SerializeField] private float startSpeed = 0f;
    [SerializeField] private bool boostFlag = false;

    private RoomManager roomManager = null;
    protected List<HitBox> enemies = null;

    protected void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    protected void OnEnable()
    {
        scaleObject.localScale = new Vector3(1f, 1f, 1f);

        enemies = roomManager.enemiesHitBox.ToList();
    }

    public void Shot(Vector3 p_position, string p_button = null, bool p_isLeft = false, float p_sizeEff = 1f)
        => StartCoroutine(ShotCo(p_position, p_button, p_isLeft, p_sizeEff));

    protected abstract IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff);

    protected virtual void SetProjectile(Vector3 p_position, bool p_isLeft, float p_sizeEff)
    {
        transform.position = p_position;
        scaleObject.localScale = new Vector3(p_isLeft ? -p_sizeEff : p_sizeEff, p_sizeEff, 1f);
        hitBox.ScaleHitBox(p_sizeEff);
        hitBox.SetDirection(p_isLeft);

        var t_dir = Direction;
        Direction = new Vector3(p_isLeft ? -t_dir.x : t_dir.x, t_dir.y, t_dir.z).normalized;
    }

    protected virtual void StartProjectile()
    {
        anim.SetTrigger("Shot");
    }

    protected abstract IEnumerator ActivateProjectile(float p_duration);

    protected IEnumerator MoveProjectile(float p_duration)
    {
        var t_timer = 0f;
        var t_speed = boostFlag ? startSpeed : 0f;

        while (t_timer < p_duration)
        {
            t_speed = boostFlag
                ? Mathf.Lerp(startSpeed, 0f, t_timer / p_duration)
                : Mathf.Lerp(0f, startSpeed, t_timer / p_duration);
            transform.position += Direction * t_speed * Time.deltaTime;
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    protected abstract IEnumerator CheckOnHit(float duration);

    protected virtual void EndProjectile()
    {
        anim.SetTrigger("End");

        ObjectPoolingManager.ReturnObject(this.gameObject);
    }
}

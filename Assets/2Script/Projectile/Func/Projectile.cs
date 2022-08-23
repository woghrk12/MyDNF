using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Animator anim = null;
    [SerializeField] protected HitBox hitBox = null;
    [SerializeField] protected Transform scaleObject = null;
    [SerializeField] protected Transform yPosObject = null;
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected int coefficient = 0;

    protected RoomManager roomManager = null;
    protected List<HitBox> targets = null;

    protected void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        targets = new List<HitBox>();
    }

    protected void OnEnable()
    {
        scaleObject.localScale = new Vector3(1f, 1f, 1f);

        targets = roomManager.Enemies.ToList();
    }

    public void Shot(Vector3 p_position, string p_button = null, bool p_isLeft = false, float p_sizeEff = 1f)
        => StartCoroutine(ShotCo(p_position, p_button, p_isLeft, p_sizeEff));

    protected abstract IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff);

    protected void SetProjectile(Vector3 p_position, bool p_isLeft, float p_sizeEff)
    {
        transform.position = p_position;
        scaleObject.localScale = new Vector3(p_isLeft ? -p_sizeEff : p_sizeEff, p_sizeEff, 1f);
        hitBox.ScaleHitBox(p_sizeEff);
        hitBox.SetDirection(p_isLeft);
    }

    protected virtual void StartProjectile()
    {
        anim.SetTrigger("Shot");
    }

    protected abstract IEnumerator ActivateProjectile(float p_duration, float p_timesValue = 1f);

    protected virtual void EndProjectile()
    {
        anim.SetTrigger("End");

        ObjectPoolingManager.ReturnObject(this.gameObject);
    }
}

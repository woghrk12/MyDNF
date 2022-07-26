using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Animator anim = null;
    [SerializeField] protected HitBox hitBox = null;
    [SerializeField] protected CharacterTransform transformController = null;
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected int coefficient = 0;
    [SerializeField] protected float startSpeed = 0f;

    protected RoomManager roomManager = null;
    protected List<HitBox> targets = null;

    protected virtual void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        targets = new List<HitBox>();
    }

    protected void OnEnable()
    {
        InitializeValue();
    }

    public void Shot(Vector3 p_position, string p_button = null, bool p_isLeft = false, float p_sizeEff = 1f)
        => StartCoroutine(ShotCo(p_position, p_button, p_isLeft, p_sizeEff));

    protected abstract IEnumerator ShotCo(Vector3 p_position, string p_button, bool p_isLeft, float p_sizeEff);

    protected virtual void InitializeValue()
    {
        transformController.LocalScale = new Vector3(1f, 1f, 1f);

        if(roomManager.Enemies != null)
            targets = roomManager.Enemies.ToList();
    }

    protected virtual void SetProjectile(Vector3 p_position, bool p_isLeft, float p_sizeEff)
    {
        transform.position = p_position;
        transformController.LocalScale = new Vector3(p_isLeft ? -p_sizeEff : p_sizeEff, p_sizeEff, 1f);
        hitBox.ScaleHitBox(p_sizeEff);
        hitBox.IsLeft = p_isLeft;
    }

    protected virtual void StartProjectile()
    {
        anim.SetTrigger("Shot");
    }

    protected virtual void EndProjectile()
    {
        anim.SetTrigger("End");

        ObjectPoolingManager.ReturnObject(this.gameObject);
    }
}

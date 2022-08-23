using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyProjectile : MonoBehaviour
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
        targets.Clear();
        targets.Add(roomManager.Player.GetComponent<HitBox>());
    }

    public void Shot()
      => StartCoroutine(ShotCo());

    private IEnumerator ShotCo()
    {
        anim.SetTrigger("Shot");
        yield return ActivateProjectile();
    }

    public virtual void SetProjectile(bool p_isLeft)
    {
        hitBox.IsLeft = p_isLeft;
    }

    protected abstract IEnumerator ActivateProjectile();
}

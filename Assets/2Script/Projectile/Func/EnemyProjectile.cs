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

    public void Shot(Vector3 p_position, bool p_isLeft)
      => StartCoroutine(ShotCo(p_position, p_isLeft));

    private IEnumerator ShotCo(Vector3 p_position, bool p_isLeft)
    {
        SetProjectile(p_position, p_isLeft);
        yield return ActivateProjectile();
    }

    public void SetProjectile(Vector3 p_position, bool p_isLeft)
    {
        transform.position = p_position;
        hitBox.IsLeft = p_isLeft;
    }

    protected abstract IEnumerator ActivateProjectile();
}

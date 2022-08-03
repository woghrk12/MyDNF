using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private string projectile = null;
    [SerializeField] private string skillMotion = null;
    [SerializeField] private float duration = 0f;
    [SerializeField] private float coefficientValue = 0f;
    [SerializeField] private float maxKeyTime = 0f;
    [SerializeField] private float coolTime = 0f;
    private float waitingTime = 0f;
    private int numOfClick = 0;

    public bool CanUse { get { return waitingTime <= 0f; } }
    public float MaxKeyTime { get { return maxKeyTime; } }
    public int NumOfClick { set { numOfClick = value; } get { return numOfClick; } }

    private void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public IEnumerator UseSkill(Animator p_anim, bool p_isLeft)
    {
        waitingTime = coolTime;

        p_anim.SetTrigger(skillMotion);
        var t_projectile = ObjectPoolingManager.SpawnObject(projectile, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.StartProjectile(p_anim.transform.position, p_isLeft);
        yield return new WaitForSeconds(duration);
    }
}


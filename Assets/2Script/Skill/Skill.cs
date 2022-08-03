using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private int numCombo = 1;
    [SerializeField] private string[] projectile = null;
    [SerializeField] private string[] skillMotion = null;
    [SerializeField] private float[] duration = null;
    [SerializeField] private float[] delay = null;
    [SerializeField] private float[] coefficientValue = null;
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
        var t_cnt = 0;

        while (t_cnt < numCombo)
        {
            p_anim.SetBool(skillMotion[t_cnt], true);

            yield return new WaitForSeconds(delay[t_cnt]);
            
            var t_projectile = ObjectPoolingManager.SpawnObject(projectile[t_cnt], Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            t_projectile.StartProjectile(p_anim.transform.position, p_isLeft);
            
            yield return new WaitForSeconds(duration[t_cnt] - delay[t_cnt]);

            p_anim.SetBool(skillMotion[t_cnt], false);

            t_cnt++;
            if (NumOfClick <= t_cnt) break;
        }
    }
}


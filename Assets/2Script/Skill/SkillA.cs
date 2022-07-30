using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillA : MonoBehaviour
{
    [SerializeField] private float damage = 0f;
    public float Damage { get { return damage; } }

    [SerializeField] private float delay = 0f;
    public float Delay { get { return delay; } }

    [SerializeField] private string projectile = "";
    public string skillMotion = "";
    [SerializeField] private float coolDown = 0f;

    private float coolTime = 0f;
    public float CoolTime { get { return coolTime; } }

    private void Update()
    {
        if(coolTime > 0f)
            coolTime -= Time.deltaTime;    
    }

    public bool UseSkill(bool p_isLeft)
    {
        if (coolTime > 0f) return false;

        var t_projectile = ObjectPoolingManager.SpawnObject(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        t_projectile.InvokeSkill(p_isLeft);
        coolTime = coolDown;
        return true;
    }    
}

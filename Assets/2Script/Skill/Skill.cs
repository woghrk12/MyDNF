using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ESkillType { SINGLE, COMBO, END }

public class Skill : MonoBehaviour
{
    public ESkillType skillType = ESkillType.SINGLE;

    [SerializeField] protected HitBox[] hitBox = null;
    [SerializeField] protected string[] skillEffect = null;
    [SerializeField] protected float[] duration = null;
    [SerializeField] protected float[] coefficientValue = null;

    [SerializeField] private float delay = 0f;

    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;

    public bool CanUse { get { return waitingTime <= 0f; } }
    public float Delay { get { return delay; } }

    private RoomManager roomManager = null;
    protected List<HitBox> enemies = null;

    protected void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    protected void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }

    public virtual IEnumerator UseSkill(bool p_isLeft)
    {
        waitingTime = coolTime;

        StartCoroutine(OnEffect(skillEffect[0], transform.position, p_isLeft, duration[0]));
        yield return CheckOnHit(hitBox[0], enemies, p_isLeft, duration[0]);
    }

    protected IEnumerator OnEffect(string p_skillEffect, Vector3 p_position, bool p_isLeft, float p_duration)
    {
        var t_effect = ObjectPoolingManager.SpawnObject(p_skillEffect, p_position, Quaternion.identity);
        t_effect.transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);

        yield return new WaitForSeconds(p_duration);

        ObjectPoolingManager.ReturnObject(t_effect);
    }

    protected IEnumerator CheckOnHit(HitBox p_hitBox, List<HitBox> p_enemies, bool p_isLeft, float p_duration)
    {
        p_hitBox.SetDirection(p_isLeft);
        p_enemies = roomManager.enemiesHitBox.ToList();

        var t_timer = 0f;

        while (t_timer <= p_duration)
        {
            p_hitBox.CalculateHitBox();
            CalculateOnHit(p_hitBox, p_enemies);
            t_timer += Time.deltaTime;
            yield return null;
        }
    }

    protected void CalculateOnHit(HitBox p_hitBox, List<HitBox> p_enemies)
    {
        var t_enemies = p_enemies;

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


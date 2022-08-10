using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetingSkill
{
    public Vector3 Target { set; get; }
    public void SetTarget();
}

public interface INonTargetingSkill
{
    public Vector3 Direction { set; get; }
    public void SetDirection();
}

public interface IComboSkill
{
    public int NumOfClick { set; get; }
    public IEnumerator CheckNumInput();
}

public interface IChargingSkill
{
    public IEnumerator CheckCharging();
}

public interface IProjectileSkill
{
    public string[] Projectile { set; get; }
}

public interface IHitboxSkill
{
    public HitBox[] HitBox { set; get; }
}

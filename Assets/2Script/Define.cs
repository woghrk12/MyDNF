using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Enum
public enum ESortingType { STATIC, UPDATE }
public enum EEnemyPatternType { IDLE, WALK, BASEATTACK, SKILL }

// Structure
[Serializable]
public struct Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}
[Serializable]
public struct EnemyPattern
{
    public EEnemyPatternType patternType;
    public float coolTime;
    public float duration;
    public float probability;
    public HitBox range;
}

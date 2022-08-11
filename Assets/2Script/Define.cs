using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Enum
public enum ESortingType { STATIC, UPDATE }

// Structure
[Serializable]
public struct Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}


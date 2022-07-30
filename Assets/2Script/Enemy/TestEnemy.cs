using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private HitBox hitBox = null;

    void Update()
    {
        hitBox.CalculateHitBox();
    }
}

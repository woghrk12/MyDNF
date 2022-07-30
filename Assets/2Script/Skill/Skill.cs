using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float coefficientValue = 0f;

    [SerializeField] protected string skillEffect = null;
    public string skillMotion = "";

    [SerializeField] protected float coolTime = 0f;
    protected float waitingTime = 0f;

    public bool CanUse { get { return waitingTime <= 0f; } }

    private void Update()
    {
        if (waitingTime > 0f)
            waitingTime -= Time.deltaTime;
    }
}


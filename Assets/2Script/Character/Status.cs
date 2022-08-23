using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int maxMana = 0;

    private int curHealth = 0;
    private int curMana = 0;

    public void InitializeValue()
    {
        curHealth = maxHealth;
        curMana = maxMana;
    }

    public bool UseMana(int p_value)
    {
        if (p_value > curMana) return false;

        curMana -= p_value;
        return true;
    }

    public bool OnDamage(int p_value)
    {
        if (p_value > curHealth) 
        {
            curHealth = 0;
            return false;
        }

        curHealth -= p_value;
        return true;
    }
}

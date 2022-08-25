using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int maxMana = 0;
    private ControlSlider healthSlider = null;
    private ControlSlider manaSlider = null;

    private int curHealth = 0;
    private int curMana = 0;

    public int CurHealth 
    {
        set 
        { 
            curHealth = value < 0 ? 0 : value;
            if(healthSlider != null)
                healthSlider.SetValue((int)((curHealth / (float)maxHealth) * 100));
        }
        get { return curHealth; } 
    }
    public int CurMana 
    { 
        set 
        { 
            curMana = value < 0 ? 0 : value;
            if (manaSlider != null)
                manaSlider.SetValue((int)((curMana / (float)maxMana) * 100));
        } 
        get { return curMana; } 
    }

    public void InitializeValue()
    {
        curHealth = maxHealth;
        curMana = maxMana;
    }

    public void SetControlSlider(ControlSlider p_healthSlider, ControlSlider p_manaSlider)
    {
        healthSlider = p_healthSlider;
        manaSlider = p_manaSlider;

        healthSlider.SetValue((int)((curHealth / (float)maxHealth) * 100));
        manaSlider.SetValue((int)((curMana / (float)maxMana) * 100));
    }

    public bool UseMana(int p_value)
    {
        if (curMana < p_value) return false;
        CurMana -= p_value;
        return true;
    }
}

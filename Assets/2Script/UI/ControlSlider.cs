using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    public void SetMaxValue(int p_value)
    {
        slider.maxValue = p_value;
        slider.value = p_value;
    }

    public void SetValue(int p_value) { slider.value = p_value; }
}

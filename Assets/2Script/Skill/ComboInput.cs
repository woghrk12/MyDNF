using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboInput : MonoBehaviour
{
    private int numOfClick = 0;
    public int NumOfClick { get { return numOfClick; } }

    private bool flag = false;
    public bool Flag { set { flag = value; } get { return flag; } }

    public IEnumerator CheckNumInput(string p_button, float p_maxComboTime)
    {
        var t_timer = 0f;

        while (t_timer <= p_maxComboTime)
        {
            if (InputManager.Buttons[p_button] == EButtonState.DOWN) numOfClick++;
            if (!flag) break;

            t_timer += Time.deltaTime;
            yield return null;
        }

        numOfClick = 0;
    }

    public void ResetValue()
    {
        numOfClick = 0;
    }
}

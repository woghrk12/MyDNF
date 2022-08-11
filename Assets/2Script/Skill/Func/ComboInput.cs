using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboInput : MonoBehaviour
{
    [SerializeField] private float maxComboTime = 0f;

    [SerializeField] private int numCombo = 0;
    public int NumCombo { get { return numCombo; } }

    private int numOfClick = 0;
    public int NumOfClick { get { return numOfClick; } }

    private bool flag = false;
    public bool Flag { set { flag = value; } get { return flag; } }

    public IEnumerator CheckNumInput(string p_button)
    {
        var t_timer = 0f;

        while (t_timer <= maxComboTime)
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

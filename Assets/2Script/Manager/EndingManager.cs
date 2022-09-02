using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject success = null;
    [SerializeField] private GameObject defeated = null;

    private void OnEnable()
    {
        StartCoroutine(GameManager.Instance.FadeIn());
        var t_flag = GameManager.Instance.IsClear;
        success.SetActive(t_flag);
        defeated.SetActive(!t_flag);
    }

    public void RestartGame()
    {
        LoadingManager.LoadScene(EScene.INGAME);
    }

    public void EndGame()
    {
        LoadingManager.LoadScene(EScene.TITLE);
    }
}

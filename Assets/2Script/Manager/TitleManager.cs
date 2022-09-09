using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	public void GameStart() => StartCoroutine(GameStartCo());

    private void Start()
    {
        StartCoroutine(GameManager.Instance.FadeIn());
    }

    private IEnumerator GameStartCo()
	{
		yield return GameManager.Instance.FadeOut();
		LoadingManager.LoadScene(EScene.INGAME);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static EScene nextScene;

    [SerializeField] private Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(EScene p_scene)
    {
        nextScene = p_scene;
        SceneManager.LoadScene((int)EScene.LOADING);
    }

    private IEnumerator LoadScene()
    {
        GameManager.Instance.OffScreen();

        var t_op = SceneManager.LoadSceneAsync((int)nextScene);
        t_op.allowSceneActivation = false;

        var t_timer = 0f;

        while (!t_op.isDone)
        {
            yield return null;

            if (t_op.progress < 0.9f)
                progressBar.fillAmount = t_op.progress;
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, t_timer);
                t_timer += Time.deltaTime;

                if (progressBar.fillAmount >= 1.0f)
                {
                    GameManager.Instance.OnScreen();
                    t_op.allowSceneActivation = true;
                }
                
            }
        }
    }
}

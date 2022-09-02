using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				var obj = FindObjectOfType<GameManager>();

				if (obj != null)
				{
					instance = obj;
				}
				else
				{
					var newObj = new GameObject().AddComponent<GameManager>();
					instance = newObj;
				}
			}
			return instance;
		}
	}

	private EControlType controlType = EControlType.KEYBOARD;
	public EControlType ControlType { get { return controlType; } }
	[SerializeField] private Image screen = null;

	private bool isClear = false;
	public bool IsClear { set { isClear = value; } get { return isClear; } }

	private void Awake()
	{
		var objs = FindObjectsOfType<GameManager>();

		if (objs.Length != 1)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
		controlType = EControlType.KEYBOARD;
#endif
#if UNITY_ANDROID
		controlType = EControlType.SCREEN;
#endif
	}

    private void Start()
    {
		StartCoroutine(FadeIn());
    }

	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

    public IEnumerator FadeIn()
	{
		OnScreen();
		yield return ChangeScreen(true);
		OffScreen();
	}
	public IEnumerator FadeOut()
	{
		OnScreen();
		yield return ChangeScreen(false);
	}
	public void OnScreen()
	{
		if (!screen.gameObject.activeSelf) screen.gameObject.SetActive(true);
	}
	public void OffScreen()
	{
		if (screen.gameObject.activeSelf) screen.gameObject.SetActive(false);
	}

	private IEnumerator ChangeScreen(bool p_isFadeIn)
	{
		float t_timer = 0f;
		float t_totalTime = 1f;
		Color t_color = screen.color;

		while (t_timer <= 1f)
		{
			t_color.a = Mathf.Lerp(p_isFadeIn ? 1f : 0f, p_isFadeIn ? 0f : 1f, t_timer / t_totalTime);
			screen.color = t_color;
			t_timer += Time.deltaTime;
			yield return null;
		}
	}
}

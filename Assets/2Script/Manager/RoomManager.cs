using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private HitBox player = null;
    public HitBox Player { get { return player; } }

    [SerializeField] private GameObject enemyList = null;

    private List<HitBox> enemies = null;
    public List<HitBox> Enemies { get { return enemies; } }

    private bool isEndGame = false;
    public bool IsEndGame 
    { 
        set 
        {
            if (isEndGame) return;
            isEndGame = value;
            StartCoroutine(EndGame(enemies.Count <= 0));
        }
        get { return isEndGame; }
    }

    private void Awake()
    {
        enemies = new List<HitBox>();
    }

    private void Start()
    {
        var t_numEnemy= enemyList.transform.childCount;

        for (int i = 0; i < t_numEnemy; i++)
            enemies.Add(enemyList.transform.GetChild(i).GetComponent<HitBox>());

        StartCoroutine(GameManager.Instance.FadeIn());
    }

    private void OnEnable()
    {
        isEndGame = false;
    }

    public void RemoveEnemy(HitBox p_target)
    {
        enemies.Remove(p_target);

        if (enemies.Count <= 0) IsEndGame = true;
    }

    private IEnumerator EndGame(bool p_flag)
    { 
        yield return GameManager.Instance.FadeOut();
        GameManager.Instance.IsClear = p_flag;
        LoadingManager.LoadScene(EScene.ENDING);
    }
}

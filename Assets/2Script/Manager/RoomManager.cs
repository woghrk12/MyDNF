using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GamePlayer player = null;
    public GamePlayer Player { get { return player; } }

    [SerializeField] private GameObject enemyList = null;

    private List<HitBox> enemies = null;
    public List<HitBox> Enemies { get { return enemies; } }
    private int numEnemies = 0;
    private int NumEnemies 
    { 
        set
        { 
            numEnemies = value;
            if (numEnemies <= 0)
                ClearRoom();
        }
        get { return numEnemies; } }

    private void Awake()
    {
        enemies = new List<HitBox>();
    }

    private void Start()
    {
        var t_numEnemy= enemyList.transform.childCount;

        for (int i = 0; i < t_numEnemy; i++)
            enemies.Add(enemyList.transform.GetChild(i).GetComponent<HitBox>());
    }

    public void RemoveEnemy(HitBox p_target)
    {
        enemies.Remove(p_target);
        NumEnemies--;
    }
    private void ClearRoom()
    { 
        
    }
}

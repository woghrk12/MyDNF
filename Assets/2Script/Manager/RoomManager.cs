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
    

    private void Awake()
    {
        enemies = new List<HitBox>();
    }

    private void Start()
    {
        var t_hitBoxes = enemyList.GetComponentsInChildren<HitBox>();

        for (int i = 0; i < t_hitBoxes.Length; i++)
            enemies.Add(t_hitBoxes[i]);
    }
}

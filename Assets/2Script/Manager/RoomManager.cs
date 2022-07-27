using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject enemies = null;
    public List<HitBox> enemiesHitBox = null;

    private void Awake()
    {
        enemiesHitBox = new List<HitBox>();
    }

    private void Start()
    {
        var t_hitBoxes = enemies.GetComponentsInChildren<HitBox>();

        for (int i = 0; i < t_hitBoxes.Length; i++)
            enemiesHitBox.Add(t_hitBoxes[i]);
    }
}

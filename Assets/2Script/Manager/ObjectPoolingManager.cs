using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    [SerializeField] private Pool[] pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> categories;

    private void Awake()
    {
        instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        categories = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        foreach (Pool t_pool in pools)
        {
            var t_categroy = new GameObject(t_pool.tag);
            t_categroy.transform.SetParent(transform);
            categories.Add(t_pool.tag, t_categroy);

            poolDictionary.Add(t_pool.tag, new Queue<GameObject>());

            for (int i = 0; i < t_pool.size; i++)
            {
                CreateNewObject(t_pool.tag, t_pool.prefab);
            }
        }
    }

    public static GameObject SpawnObject(string p_tag, Vector3 p_position, Quaternion p_rotation)
        => instance.SpawnFromPool(p_tag, p_position, p_rotation);
    public static void ReturnObject(GameObject p_obj)
        => instance.ReturnToPool(p_obj);
    
    private GameObject SpawnFromPool(string p_tag, Vector3 p_position, Quaternion p_rotation)
    {
        Queue<GameObject> t_poolQueue = poolDictionary[p_tag];

        if (t_poolQueue.Count <= 0)
        {
            var t_pool = Array.Find(pools, x => x.tag == p_tag);
            CreateNewObject(t_pool.tag, t_pool.prefab);
        }

        var t_obj = t_poolQueue.Dequeue();
        t_obj.transform.position = p_position;
        t_obj.transform.rotation = p_rotation;
        t_obj.SetActive(true);

        return t_obj;
    }

    private void ReturnToPool(GameObject p_obj)
    {
        p_obj.SetActive(false);
        poolDictionary[p_obj.name].Enqueue(p_obj);
    }

    private void CreateNewObject(string p_tag, GameObject p_prefab)
    {
        var t_obj = Instantiate(p_prefab, transform);
        t_obj.name = p_tag;
        t_obj.transform.SetParent(categories[p_tag].transform);
        poolDictionary[p_tag].Enqueue(t_obj);
        t_obj.SetActive(false);
    }
}

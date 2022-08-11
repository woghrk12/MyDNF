using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingManager : MonoBehaviour
{
    [SerializeField] private Transform back = null;
    [SerializeField] private Transform front = null;

    public int GetSortingOrder(Transform p_obj)
    {
        float t_objDist = Mathf.Abs(back.position.y - p_obj.position.y);
        float t_totalDist = Mathf.Abs(back.position.y - front.position.y);

        return (int)(Mathf.Lerp(System.Int16.MinValue, System.Int16.MaxValue, t_objDist / t_totalDist));
    }
}

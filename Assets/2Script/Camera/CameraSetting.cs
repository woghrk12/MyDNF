using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityScreen = UnityEngine.Screen;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] private float camWidth = 0;
    [SerializeField] private float camHeight = 0;
    private Camera mainCam = null;

    private void Awake()
    {
        mainCam = Camera.main;

        SetResolution();
    }

    private void SetResolution()
    {
        var t_rect = mainCam.rect;
        var t_targetRate = camWidth / camHeight;
        var t_screenRate = (float)UnityScreen.width / UnityScreen.height;

        if (t_targetRate < t_screenRate) t_rect.width = t_targetRate / t_screenRate;
        else t_rect.height = t_screenRate / t_targetRate;
 
        t_rect.x = (1f - t_rect.width) * 0.5f; t_rect.y = (1f - t_rect.height) * 0.5f;

        mainCam.rect = t_rect;
    }
}

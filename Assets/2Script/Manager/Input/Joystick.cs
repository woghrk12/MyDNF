using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 Direction { get { return new Vector2(inputDir.x, inputDir.y); } }

    [SerializeField] private RectTransform background = null;
    [SerializeField] private RectTransform handle = null;

    private Vector2 inputDir = Vector2.zero;
    private Vector2 radius = Vector2.zero;

    [SerializeField] private float handleRange = 0f;
    [SerializeField, Range(0, 1)] private float minValue = 0f;
    [SerializeField, Range(0, 1)] private float maxValue = 0f;

    private void Awake()
    {
        radius = background.sizeDelta / 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDir = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    private void ControlLever(PointerEventData p_eventData)
    { 
        inputDir = (p_eventData.position - (Vector2)background.position) / radius;
        HandleInput(inputDir.magnitude);
        handle.anchoredPosition = inputDir * radius * handleRange;
    }

    private void HandleInput(float p_magnitude)
    {
        if (p_magnitude > minValue)
        {
            if (p_magnitude > maxValue)
                inputDir = inputDir.normalized * maxValue;
        }
        else
            inputDir = Vector2.zero;
    }
}

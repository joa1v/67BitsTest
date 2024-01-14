using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject _joystick;
    [SerializeField] private RectTransform _joystickRectTransform;
    [SerializeField] private OnScreenStick _stick;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        _joystick.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _joystick.SetActive(true);
        _joystickRectTransform.anchoredPosition = GetMousePositionInRectrangle(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _stick.OnPointerUp(eventData);
        _joystick.SetActive(false);
    }

    private Vector2 GetMousePositionInRectrangle(Vector2 mousePos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, mousePos, null, out Vector2 localPoint);
        return localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _stick.OnDrag(eventData);
    }
}

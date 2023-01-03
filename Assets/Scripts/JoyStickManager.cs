using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickManager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public GameObject lever;
    private RectTransform joystickRect;
    private Image joystickImage;
    private RectTransform leverRect;
    private Image leverImage;

    public float limitRange;

    public Vector2 axis;
    public bool isDrag;

    private void Awake()
    {
        joystickRect = GetComponent<RectTransform>();
        joystickImage = GetComponent<Image>();
        leverRect = lever.GetComponent<RectTransform>();
        leverImage = lever.GetComponent<Image>();
    }

    private void Start()
    {
        OffAlpha();
    }

    private void Update()
    {
        MoveJoyStick();
    }

    private void MoveJoyStick()
    {
        Vector2 mousePos = Input.mousePosition;
        if(!isDrag)
            joystickRect.position = mousePos;
    }

    private void SetAxis(PointerEventData eventData)
    {
        Vector2 eventPos = eventData.position;
        Vector2 joystickPos = joystickRect.position;
        Vector2 offset = eventPos - joystickPos;
        axis = (eventPos - joystickPos).normalized;
        leverRect.position = offset.magnitude < limitRange ? eventPos : joystickPos + axis * limitRange;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        SetAxis(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
       OnAlpha();
        SetAxis(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
       OffAlpha();
        leverRect.anchoredPosition = Vector2.zero;
        axis = Vector2.zero;
    }

    private void OnAlpha()
    {
        Color color = joystickImage.color;
        Color color2 = leverImage.color;
        color.a = 1f;
        color2.a = 1f;
        joystickImage.color = color;
        leverImage.color = color2;
    }

    private void OffAlpha()
    {
        Color color = joystickImage.color;
        Color color2 = leverImage.color;
        color.a = 0f;
        color2.a = 0f;
        joystickImage.color = color;
        leverImage.color = color2;
    }
}

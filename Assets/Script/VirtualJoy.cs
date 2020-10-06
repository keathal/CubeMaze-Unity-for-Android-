using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoy : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Image BgImg;
    public Image JoyImg;
    public Vector2 InputDirections { set; get; }
     void Start()
    {
        
        BgImg = GetComponent<Image>();
        JoyImg = transform.GetChild(0).GetComponent<Image>();
        InputDirections = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 Pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle 
            (BgImg.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out Pos))
        {
            Pos.x = (Pos.x / BgImg.rectTransform.sizeDelta.x);
            Pos.y = (Pos.y / BgImg.rectTransform.sizeDelta.y);
            float x = (BgImg.rectTransform.pivot.x == 1) ? Pos.x * 2 + 1 : Pos.x * 2 - 1f;
            float y = (BgImg.rectTransform.pivot.y == 1) ? Pos.y * 2 + 1 : Pos.y * 2 - 1f;
            InputDirections = new Vector2(x, y);
            InputDirections = (InputDirections.magnitude > 1) ? InputDirections.normalized : InputDirections;
            JoyImg.rectTransform.anchoredPosition = new Vector2(InputDirections.x * (BgImg.rectTransform.sizeDelta.x / 3),
            InputDirections.y * (BgImg.rectTransform.sizeDelta.y / 3));
        }
            

    }
    public virtual void OnPointerDown (PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirections = Vector2.zero;
        JoyImg.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        if (InputDirections.x != 0)
            return InputDirections.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (InputDirections.y != 0)
            return InputDirections.y;
        else
            return Input.GetAxis("Vertical");
    }
}

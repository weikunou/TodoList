using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Button button;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        button = transform?.GetComponent<Button>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(button)
        {
            button.interactable = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(button)
        {
            button.interactable = true;
        }
    }
}

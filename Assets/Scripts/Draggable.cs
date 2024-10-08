using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class Draggable : MonoBehaviour, IPointerEnterHandler, IDragHandler, IPointerExitHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        transform.position = eventData.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer on me");
        transform.localScale = Vector3.one * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}

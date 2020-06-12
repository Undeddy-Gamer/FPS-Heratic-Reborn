using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform dropTransform = (RectTransform)transform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(dropTransform, Input.mousePosition))
        {
            Debug.Log("dropped");
        }
    }
}

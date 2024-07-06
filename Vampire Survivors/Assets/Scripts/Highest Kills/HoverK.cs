using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class HoverK : UnityEngine.MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        HoverText.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Map.Instance.SelectSprite(this);
        GetComponent<Image>().color = new Color(0, 178, 0, 50);
    }


    public void Deselect()
    {
        GetComponent<Image>().color = Color.white;
    }
}

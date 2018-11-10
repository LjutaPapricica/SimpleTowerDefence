using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteScript : MonoBehaviour, IPointerClickHandler
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Map.Instance.SelectSprite(this);
        image.color = new Color(0, 178, 0, 50);
    }


    public void Deselect()
    {
        image.color = Color.white;
    }
}

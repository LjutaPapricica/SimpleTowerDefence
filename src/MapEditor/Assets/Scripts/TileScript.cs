using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour, IPointerClickHandler
{
    public TileScript Initialize(int x, int y)
    {
        X = x;
        Y = y;

        return this;
    }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Map.Instance.SelectTile(this);
    }

    public void ChangeSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private Image image;
    public int X { get; set; }
    public int Y { get; set; }

}
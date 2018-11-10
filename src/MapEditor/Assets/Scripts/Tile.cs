using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public Tile Initialize(int x, int y)
    {
        X = x;
        Y = y;

        return this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.blue;
    }

    public Image Image { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

}
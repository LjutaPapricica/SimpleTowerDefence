using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour, IPointerClickHandler
{
    public string Type
    {
        get
        {
            return image.name;
        }
    }
    
    public List<GameObject> NestedImages { get; set; }

    public TileScript Initialize(int x, int y)
    {
        X = x;
        Y = y;

        return this;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        NestedImages = new List<GameObject>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Map.Instance.SelectTile(this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            Map.Instance.ContextTile(this);
    }

    public void ChangeSprite(Sprite sprite)
    {
        image.name = sprite.name;
        image.sprite = sprite;
        ClearNested();
    }

    public void AddImage(Sprite sprite)
    {
        GameObject imageObject = ImageHelper.CreateImage(sprite, image.transform, sprite.name, image.transform.position, new Vector3(0.3f, 0.3f));
        NestedImages.Add(imageObject);
    }

    public void ClearNested()
    {
        foreach (GameObject im in NestedImages)
            Destroy(im);

        NestedImages.Clear();
    }

    private Image image;
    public int X { get; set; }
    public int Y { get; set; }

}
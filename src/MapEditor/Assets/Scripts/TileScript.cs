using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour, IPointerClickHandler
{
    private List<GameObject> nestedImages = new List<GameObject>();
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
        if (eventData.button == PointerEventData.InputButton.Left)
            Map.Instance.SelectTile(this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            Map.Instance.AddToTile(this);
    }

    public void ChangeSprite(Sprite sprite)
    {
        image.sprite = sprite;
        ClearNested();
    }

    public void AddImage(Sprite sprite)
    {
        GameObject imageObject = new GameObject();
        Image nested = imageObject.AddComponent<Image>();
        nested.sprite = sprite;
        imageObject.name = "Image";
        imageObject.transform.SetParent(image.transform);
        imageObject.transform.position = image.transform.position;
        imageObject.transform.localScale = new Vector3(0.3f, 0.3f);
        imageObject.SetActive(true);

        nestedImages.Add(imageObject);
    }

    public void ClearNested()
    {
        foreach (GameObject im in nestedImages)
            Destroy(im);

        nestedImages.Clear();
    }

    private Image image;
    public int X { get; set; }
    public int Y { get; set; }

}
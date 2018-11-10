
using UnityEngine;
using UnityEngine.UI;

public static  class ImageHelper
{
    public static GameObject CreateImage(Sprite sprite, Transform parent)
    {
        GameObject imageObject = new GameObject();
        imageObject.name = "Image";
        imageObject.transform.SetParent(parent);
        imageObject.SetActive(true);
        
        Image image = imageObject.AddComponent<Image>();
        image.sprite = sprite;
        
        return imageObject;
    }

    public static GameObject CreateImage(Sprite sprite, Transform parent, Vector3 scale)
    {
        GameObject imageObject = CreateImage(sprite, parent);
        imageObject.GetComponent<Image>().transform.localScale = scale;

        return imageObject;
    }

    public static GameObject CreateImage(Sprite sprite, Transform parent, Vector3 position, Vector3 scale)
    {
        GameObject imageObject = CreateImage(sprite, parent);
        Image image = imageObject.GetComponent<Image>();
        image.transform.localScale = scale;
        image.transform.position = position;
    
        return imageObject;
    }
}

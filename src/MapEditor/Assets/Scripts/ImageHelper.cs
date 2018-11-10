
using UnityEngine;
using UnityEngine.UI;

public static  class ImageHelper
{
    public static GameObject CreateImage(Sprite sprite, Transform parent, string name)
    {
        GameObject imageObject = new GameObject();
        imageObject.name = name;
        imageObject.transform.SetParent(parent);
        imageObject.SetActive(true);
        
        Image image = imageObject.AddComponent<Image>();
        image.sprite = sprite;
        
        return imageObject;
    }

    public static GameObject CreateImage(Sprite sprite, Transform parent, string name, Vector3 scale)
    {
        GameObject imageObject = CreateImage(sprite, parent, name);
        imageObject.GetComponent<Image>().transform.localScale = scale;

        return imageObject;
    }

    public static GameObject CreateImage(Sprite sprite, Transform parent, string name, Vector3 position, Vector3 scale)
    {
        GameObject imageObject = CreateImage(sprite, parent, name);
        Image image = imageObject.GetComponent<Image>();
        image.transform.localScale = scale;
        image.transform.position = position;
    
        return imageObject;
    }
}

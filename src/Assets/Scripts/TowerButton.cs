using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject button;

    public GameObject Button
    {
        get
        {
            return button;
        }
    }

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

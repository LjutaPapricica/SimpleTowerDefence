using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    private GameObject range;

    private bool isSelected;

    // Use this for initialization
    void Start()
    {
        range = transform.GetChild(0).gameObject;
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Toggle();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
    }

    public void Select()
    {
        foreach (TileScript tile in FloorManager.Instance.TileScripts.Values)
        {
            if (tile.Tower != null)
                tile.Tower.Deselect();
        }
        isSelected = true;
        range.GetComponent<SpriteRenderer>().enabled = true;


    }

    public void Deselect()
    {
        isSelected = false;
        if (range != null)
            range.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Toggle()
    {
        if (isSelected)
            Deselect();
        else
            Select();
    }
}

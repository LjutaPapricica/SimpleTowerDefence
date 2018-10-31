using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    private GameObject range;
    private Range rangeScript;

    private bool isSelected = true;
    
    // Use this for initialization
    void Start()
    {
        range = transform.GetChild(0).gameObject;
        rangeScript = range.GetComponent<Range>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Toggle();
        }
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

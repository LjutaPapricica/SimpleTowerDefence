using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    private GameObject range;

    private bool isSelected;
    private Mob target;
    private Queue<Mob> targets = new Queue<Mob>();

    // Use this for initialization
    void Start()
    {
        range = transform.GetChild(0).gameObject;
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Toggle();
        }
    }

    private void Attack()
    {
        if (target == null && targets.Count > 0)
        {
            target = targets.Dequeue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mob")
        {
            targets.Enqueue(collision.GetComponent<Mob>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Mob")
        {
            target = null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private bool selected;
    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    private GameObject hoverObject;
    public void Activate(GameObject prefab)
    {
        hoverObject = Instantiate(prefab, transform);
        hoverObject.GetComponent<Tower>().Hover = true;
    }

    public void Deactivate()
    {
        GameManager.Instance.ClickedButton = null;
        Destroy(hoverObject);
    }
}

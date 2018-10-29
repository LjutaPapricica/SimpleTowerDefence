using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private TowerButton clickedButton;
    public TowerButton ClickedButton
    {
        get
        {
            return clickedButton;
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

    public void PickTower(TowerButton towerButton)
    {
        clickedButton = towerButton;
        Hover.Instance.Activate(towerButton.Sprite);
    }

    public void BuyTower()
    {
        clickedButton = null;
    }

    private void HandleEscape()
    {

    }
}

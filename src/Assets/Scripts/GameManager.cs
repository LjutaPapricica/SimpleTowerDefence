using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedButton { get; set; }

    [SerializeField]
    private Text currencyText;

    private int currency;
    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            currency = value;
            currencyText.text = value + "<color=\"lime\">$</color>";
        }
    }

    // Use this for initialization
    void Start()
    {
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    } 

    public void PickTower(TowerButton towerButton)
    {
        ClickedButton = towerButton;
        Hover.Instance.Activate(towerButton.Sprite);
    }

    public void BuyTower()
    {
        Hover.Instance.Deactivate();
    }

    private void HandleEscape()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}

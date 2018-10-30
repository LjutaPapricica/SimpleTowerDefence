using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedButton { get; set; }
    public ObjectPool ObjectPool { get; set; }

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
        if (currency >= towerButton.Price)
        {
            ClickedButton = towerButton;
            Hover.Instance.Activate(towerButton.Sprite);
        }
    }

    public void BuyTower()
    {
        if (currency >= ClickedButton.Price)
        {
            Currency -= ClickedButton.Price;
            Hover.Instance.Deactivate();
        }
    }

    private void HandleEscape()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int mobIndex = Random.Range(0, 2);
        string type = string.Empty;

        switch (mobIndex)
        {
            case 0:
                type = "GreenTank";
                break;
            case 1:
                type = "WhiteTank";
                break;
        }

        Mob mob = ObjectPool.GetObject(type).GetComponent<Mob>();
        mob.Spawn();
        
        yield return new WaitForSeconds(2.5f);
    }

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }
}

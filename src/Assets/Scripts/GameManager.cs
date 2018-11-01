using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedButton { get; set; }
    public ObjectPool ObjectPool { get; set; }

    [SerializeField]
    private GameObject waveButton;

    private List<Mob> activeMobs = new List<Mob>();

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

    public bool IsWaveActive
    {
        get
        {
            return activeMobs.Count > 0;
        }
    }


    [SerializeField]
    private Text waveText;

    private int waveNumber;
    public int WaveNumber
    {
        get
        {
            return waveNumber;
        }
        set
        {
            waveNumber = value;
            waveText.text = "Wave: <color=\"lime\">" + value + "</color>";
        }
    }

    // Use this for initialization
    void Start()
    {
        Currency = 25;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerButton)
    {
        if (currency >= towerButton.Price && !IsWaveActive)
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
        ++WaveNumber;
        waveButton.SetActive(false);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < 1; ++i)
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
            mob.Path = FloorManager.Instance.FinalPath;
            mob.Spawn();
            mob.name = "Mob";

            activeMobs.Add(mob);

            yield return new WaitForSeconds(2.5f);
        }
    }

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void RemoveMonster(Mob monster)
    {
        activeMobs.Remove(monster);
        if (!IsWaveActive)
            waveButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text priceText;

    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.OnCurrencyChanged += OnCurrencyChanged;
        priceText.text = price + " $";
    }

    private void OnCurrencyChanged(object sender, System.EventArgs e)
    {
        if (price > GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

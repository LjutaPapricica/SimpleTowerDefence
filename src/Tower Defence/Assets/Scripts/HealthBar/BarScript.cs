using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    private Image fill;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private float Normalize(float value, float min, float max, float outMin, float outMax)
    {
        return (value - min) * (outMax - outMin) / (max - min) + outMin;
    }

    public void ChangeAmount(float amount, float min, float max)
    {
        fill.fillAmount = Normalize(amount, min, max, 0, 1);
    }
}

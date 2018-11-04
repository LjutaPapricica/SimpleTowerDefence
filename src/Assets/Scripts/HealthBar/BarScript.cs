using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image fill;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = fillAmount;
    }

    private float Normalize(float value, float min, float max, float outMin, float outMax)
    {
        return (value - min) * (outMax - outMin) / (max - min) + outMin;
    }
}

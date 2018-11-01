using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberManager : Singleton<NumberManager>
{
    [SerializeField]
    private Sprite[] numberPrefabs;

    public Sprite GetNumber(int index)
    {
        return numberPrefabs[index];
    }
}

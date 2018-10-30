using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public void Spawn()
    {
        transform.position = FloorManager.Instance.StartPoint.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    private List<GameObject> prefabs;

    public GameObject GetObject(string type)
    {
        GameObject prefab = prefabs.First(o => o.name == type);

        GameObject targetObject = Instantiate(prefab);
        targetObject.name = type;
        return targetObject;
    }
}

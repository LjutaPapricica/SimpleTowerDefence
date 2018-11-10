using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    private List<GameObject> prefabs;

    private List<GameObject> pooled = new List<GameObject>();

    public GameObject GetObject(string type)
    {
        GameObject fromPool = pooled.FirstOrDefault(o => o.name == type);
        if (fromPool != null && !fromPool.activeInHierarchy)
        {
            fromPool.SetActive(true);
            return fromPool;
        }
        
        GameObject prefab = prefabs.First(o => o.name == type);

        GameObject targetObject = Instantiate(prefab);
        targetObject.name = type;
        pooled.Add(targetObject);
        return targetObject;
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

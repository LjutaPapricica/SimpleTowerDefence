using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Mob Target { get; set; }
    public float Speed { get; set; }
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (Target != null && Target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
        else if (!Target.IsActive)
        {
            GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        }
    }
}

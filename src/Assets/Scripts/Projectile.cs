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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mob")
        {
            Debug.Log(collision);

            GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        }
    }
    
    private void MoveToTarget()
    {
        if (Target != null && Target.IsActive)
        {
            transform.LookAt(Target.transform);
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.forward * Speed, ForceMode2D.Force);
           // transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
        else if (Target != null && !Target.IsActive)
        {
            GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        }
    }
}

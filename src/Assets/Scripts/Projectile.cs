using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Mob Target { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public ElementType ElementType { get; set; }

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

            Target.GetComponent<Mob>().TakeDamage(Damage, ElementType);
            GetComponent<Animator>().SetTrigger("Impact");
        }
    }

    private void MoveToTarget()
    {
        if (Target != null && Target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);

            Vector2 dir = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
        else if (Target != null && !Target.IsActive)
        {
            GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        }
    }
}

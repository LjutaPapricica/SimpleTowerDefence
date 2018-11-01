using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    private GameObject range;

    private bool isSelected;
    private Mob target;
    private Queue<Mob> targets = new Queue<Mob>();
    private bool canAttack = true;

    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float speed;
    private float attackTimer;

    // Use this for initialization
    void Start()
    {
        range = transform.GetChild(0).gameObject;
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > cooldown)
            {
                attackTimer = 0;
                canAttack = true;
            }
        }
        if (target == null && targets.Count > 0)
        {
            Debug.Log("Target active");
            target = targets.Dequeue();
        }
        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.ObjectPool.GetObject("Fire").GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Speed = speed;
        projectile.Target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mob")
        {
            Debug.Log("Enqueue target");
            targets.Enqueue(collision.GetComponent<Mob>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Mob")
        {
            Debug.Log("Target inactive");
            target = null;
        }
    }

    public void Select()
    {
        foreach (TileScript tile in FloorManager.Instance.TileScripts.Values)
        {
            if (tile.Tower != null)
                tile.Tower.Deselect();
        }
        isSelected = true;
        range.GetComponent<SpriteRenderer>().enabled = true;


    }

    public void Deselect()
    {
        isSelected = false;
        if (range != null)
            range.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Toggle()
    {
        if (isSelected)
            Deselect();
        else
            Select();
    }
}

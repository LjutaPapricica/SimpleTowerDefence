using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ElementType
{
    None,
    Storm,
    Fire,
    Frost,
    Poison
}

public abstract class Tower : MonoBehaviour
{
    private GameObject range;

    private bool isSelected;
    private Mob target;
    private Queue<Mob> targets = new Queue<Mob>();
    private bool canAttack = true;

    public ElementType ElementType { get; protected set; }

    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private string projectileName;

    private float attackTimer;

    public int Price { get; set; }

    // Use this for initialization
    public virtual void Start()
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
            StartCoroutine(Rotate(transform.position, target.transform.position));
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
    }

    private IEnumerator Rotate(Vector2 from, Vector2 to)
    {
        float progress = 0;

        Vector2 direction = to - from;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        while (progress <= 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, progress);
            progress += Time.deltaTime;

            yield return null;
        }

        transform.rotation = rotation;
    }

    private IEnumerator ResetRotation(Vector2 from)
    {
        float progress = 0;

        while (progress <= 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, progress);
            progress += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.identity;
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.ObjectPool.GetObject(projectileName).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Speed = speed;
        projectile.Target = target;
        projectile.Damage = damage;
        projectile.ElementType = ElementType;
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

            if (targets.Count == 0)
                StartCoroutine(ResetRotation(transform.position));
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

        GameManager.Instance.SelectedTower = this;
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

        GameManager.Instance.ToggleUpdatePanel();
    }
}

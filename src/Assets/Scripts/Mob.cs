using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public bool IsActive { get; set; }
    private Stack<Node> path;
    public Stack<Node> Path
    {
        get
        {
            return path;
        }
        set
        {
            path = new Stack<Node>(value);

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }
    
    public Point GridPosition { get; set; }
    private Vector3 destination;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    public void Spawn()
    {
        transform.position = FloorManager.Instance.StartPoint.transform.position;
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));

        Path = FloorManager.Instance.FinalPath;
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove = false)
    {
        float progress = 0;

        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;
        if (remove)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
        }
    }
}

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

    public enum Direction
    {
        None = -1,
        Left,
        Up,
        Right,
        Down
    }

    private Direction direction = Direction.Right;
    bool rotated;

    private void Move()
    {
        if (IsActive)
        {
            int angles = 0;

            if (rotated)
            {
                rotated = false;
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    switch (direction)
                    {
                        case Direction.Right:
                            if (path.Peek().GridPosition.Y == GridPosition.Y)
                            {
                                if (path.Peek().GridPosition.X > GridPosition.X)
                                {
                                    direction = Direction.Down;
                                    angles = -90;
                                }
                                else if (path.Peek().GridPosition.X < GridPosition.X)
                                {
                                    direction = Direction.Up;
                                    angles = 90;
                                }
                            }
                            break;

                        case Direction.Left:
                            if (path.Peek().GridPosition.Y == GridPosition.Y)
                            {
                                if (path.Peek().GridPosition.X > GridPosition.X)
                                {
                                    direction = Direction.Up;
                                    angles = 90;
                                }
                                else if (path.Peek().GridPosition.X < GridPosition.X)
                                {
                                    direction = Direction.Down;
                                    angles = -90;
                                }
                            }
                            break;

                        case Direction.Up:
                            if (path.Peek().GridPosition.X == GridPosition.X)
                            {
                                if (path.Peek().GridPosition.Y > GridPosition.Y)
                                {
                                    direction = Direction.Right;
                                    angles = -90;
                                }
                                else if (path.Peek().GridPosition.Y < GridPosition.Y)
                                {
                                    direction = Direction.Left;
                                    angles = 90;
                                }
                            }
                            break;

                        case Direction.Down:
                            if (path.Peek().GridPosition.X == GridPosition.X)
                            {
                                if (path.Peek().GridPosition.Y > GridPosition.Y)
                                {
                                    direction = Direction.Left;
                                    angles = 90;
                                }
                                else if (path.Peek().GridPosition.Y < GridPosition.Y)
                                {
                                    direction = Direction.Right;
                                    angles = -90;
                                }
                            }
                            break;
                    }

                    if (angles > 0)
                    {
                        Vector3 toTarget = destination - transform.position;
                        float angle = Mathf.Atan2(toTarget.y, toTarget.x) + Mathf.Rad2Deg + 90;
                        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, 20 * Time.deltaTime);
                        //rotated = true;
                    }

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
            Release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
        }
    }

    private void Release()
    {
        IsActive = false;
        GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }
}

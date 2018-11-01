﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float health;
    
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
    private float angles;

    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    if (DetermineRotation())
                        StartCoroutine(Rotate());

                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    private IEnumerator Rotate()
    {
        float progress = 0;

        Quaternion rotation = Quaternion.AngleAxis(angles, Vector3.forward);

        while (progress < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, progress);

            progress += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotation;
    }

    private bool DetermineRotation()
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
                        return true;
                    }
                    else if (path.Peek().GridPosition.X < GridPosition.X)
                    {
                        direction = Direction.Up;
                        angles = 90;
                        return true;
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
                        return true;
                    }
                    else if (path.Peek().GridPosition.X < GridPosition.X)
                    {
                        direction = Direction.Down;
                        angles = -90;
                        return true;
                    }
                }
                break;

            case Direction.Up:
                if (path.Peek().GridPosition.X == GridPosition.X)
                {
                    if (path.Peek().GridPosition.Y > GridPosition.Y)
                    {
                        direction = Direction.Right;
                        angles = 0;
                        return true;
                    }
                    else if (path.Peek().GridPosition.Y < GridPosition.Y)
                    {
                        direction = Direction.Left;
                        angles = 180;
                        return true;
                    }
                }
                break;

            case Direction.Down:
                if (path.Peek().GridPosition.X == GridPosition.X)
                {
                    if (path.Peek().GridPosition.Y > GridPosition.Y)
                    {
                        direction = Direction.Right;
                        angles = 0;
                        return true;
                    }
                    else if (path.Peek().GridPosition.Y < GridPosition.Y)
                    {
                        direction = Direction.Left;
                        angles = 180;
                        return true;
                    }
                }
                break;
        }

        return false;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Release();
    }
}

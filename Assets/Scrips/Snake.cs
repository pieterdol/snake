using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public bool autoPlayEnabled = false;
    public Vector3 startPosition;
    public SnakeBlock snakeBlockPrefab;

    public enum Directions
    {
        Up,
        Down,
        Right,
        Left
    };

    // This direction always stays the same and is use when starting and resetting the game
    public Directions startDirection = Directions.Right;

    // This direction is changed anytime the user presses a key (up, down, left or right)
    private Directions direction = Directions.Right;

    // This direction changes when calling Move(), it gets set to this.direction
    // This direction is also used to check which direction the snake can go, in GameManager
    private Directions currentDirection = Directions.Right;

    public float moveEveryXSeconds = 0.1f;
    public int blockStartAmount = 3;

    private bool grow = false;
    private bool isPlaying = true;

    private static List<SnakeBlock> snakeBlocks = new List<SnakeBlock>();

    // Use this for initialization
    void Start()
    {
        CreateInitialBlocks(startPosition);

        Invoke("Move", moveEveryXSeconds);
    }

    private void CreateInitialBlocks(Vector3 position)
    {
        snakeBlocks = new List<SnakeBlock>();
        for (int i = 0; i < blockStartAmount; i++) {
            SnakeBlock snakeBlock = CreateBlock(position);
            position = snakeBlock.Position();
            // Subtract one unit for every block
            position.x -= 1;
        }

        ConnectSnakeBlocks();
    }

    internal void SetDirection(Directions setToDirection)
    {
        direction = setToDirection;
    }

    internal Directions CurrentDirection()
    {
        return currentDirection;
    }

    private SnakeBlock CreateBlock(Vector3 position)
    {
        SnakeBlock snakeBlock = Instantiate(
                snakeBlockPrefab,
                position,
                Quaternion.identity
            ) as SnakeBlock;
        snakeBlock.transform.parent = transform;
        snakeBlock.SetPosition(position);

        snakeBlocks.Add(snakeBlock);

        return snakeBlock;
    }

    internal static List<SnakeBlock> SnakeBlocks()
    {
        return snakeBlocks;
    }

    public void Grow()
    {
        grow = true;
    }

    public void Move()
    {
        if (autoPlayEnabled) {
            SetDirectionToFood();
        }

        currentDirection = direction;
        Vector3 newPosition = GetNewPositionForFirstBlock(transform.GetChild(0).position);
        foreach (Transform snakeBlock in transform) {
            Vector3 oldPosition = snakeBlock.position;
            snakeBlock.position = newPosition;
            newPosition = oldPosition;
        }
        if (grow) {
            grow = false;
            CreateBlock(newPosition);
        }
        ConnectSnakeBlocks();
        if (isPlaying) {
            Invoke("Move", moveEveryXSeconds);
        }
    }

    internal void StartMoving()
    {
        isPlaying = true;
        CancelInvoke("Move");
        Invoke("Move", 0.5f);
    }

    internal void StopMoving()
    {
        isPlaying = false;
        CancelInvoke("Move");
    }

    internal void Reset()
    {
        foreach (SnakeBlock snakeBlock in snakeBlocks) {
            Destroy(snakeBlock.gameObject);
        }
        snakeBlocks.Clear();
        direction = startDirection;
        currentDirection = startDirection;
        CreateInitialBlocks(startPosition);
    }

    private void SetDirectionToFood()
    {
        Food food = FindObjectOfType<Food>();
        if (!food) {
            return;
        }

        Transform snakeHead = transform.GetChild(0);

        if (snakeHead.position.x > food.transform.position.x) {
            direction = Directions.Left;
        } else if (snakeHead.position.x < food.transform.position.x) {
            direction = Directions.Right;
        } else if (snakeHead.position.y > food.transform.position.y) {
            direction = Directions.Down;
        } else if (snakeHead.position.y < food.transform.position.y) {
            direction = Directions.Up;
        }
    }

    private void ConnectSnakeBlocks()
    {
        int snakeBlocksCount = snakeBlocks.Count;

        for (int i = 0; i < snakeBlocksCount; i++) {
            snakeBlocks[i].HideAllParts();
            // Connect to previous part
            if (i > 0) {
                snakeBlocks[i].ConnectTo(snakeBlocks[i - 1]);
            }
            // Connect to next part
            if (i < snakeBlocksCount - 1) {
                snakeBlocks[i].ConnectTo(snakeBlocks[i + 1]);
            }
        }
    }

    private Vector3 GetNewPositionForFirstBlock(Vector3 currentPosition)
    {
        switch (direction) {
            case Directions.Left:
                currentPosition.x -= 1;
                break;
            case Directions.Right:
                currentPosition.x += 1;
                break;
            case Directions.Up:
                currentPosition.y += 1;
                break;
            case Directions.Down:
                currentPosition.y -= 1;
                break;
        }

        return currentPosition;
    }
}

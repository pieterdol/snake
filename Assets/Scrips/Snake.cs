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
    public Directions direction = Directions.Right;
    public Directions currentDirection = Directions.Right;

    public float moveEveryXSeconds = 0.1f;
    public int blockStartAmount = 3;

    private bool isAlive = true;

    private bool grow = false;

    // Use this for initialization
    void Start ()
    {
        CreateInitialBlocks(startPosition);

        Invoke("Move", moveEveryXSeconds);
    }

    private void CreateInitialBlocks(Vector3 position)
    {
        for (int i = 0; i < blockStartAmount; i++) {
            SnakeBlock snakeBlock = CreateBlock(position);
            position = snakeBlock.Position();
            // Subtract one unit for every block
            position.x -= (i + 1);
        }

        ConnectSnakeBlocks();
    }

    private SnakeBlock CreateBlock(Vector3 position)
    {
        SnakeBlock snakeBlock = Instantiate(
                snakeBlockPrefab,
                position,
                Quaternion.identity
            ) as SnakeBlock;
        snakeBlock.transform.parent = transform;

        return snakeBlock;
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
            CreateBlock(newPosition);
            grow = false;
        }
        ConnectSnakeBlocks();

        if (isAlive) {
            Invoke("Move", moveEveryXSeconds);
        }
    }

    private void SetDirectionToFood()
    {
        Food food = FindObjectOfType<Food>();

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
        SnakeBlock[] snakeBlocks = FindObjectsOfType<SnakeBlock>();
        int snakeBlocksCount = snakeBlocks.Length;

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

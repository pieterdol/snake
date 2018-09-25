using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
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

    public float moveEveryXSeconds = 0.1f;
    public int blockStartAmount = 3;

    private bool isAlive = true;

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

    public void Move()
    {
        Vector3 newPosition = GetNewPositionForFirstBlock(transform.GetChild(0).position);
        foreach (Transform snakeBlock in transform) {
            Vector3 oldPosition = snakeBlock.position;
            snakeBlock.position = newPosition;
            newPosition = oldPosition;
        }
        ConnectSnakeBlocks();

        if (isAlive) {
            Invoke("Move", moveEveryXSeconds);
        }
    }

    private void ConnectSnakeBlocks()
    {
        SnakeBlock[] snakeBlocks = FindObjectsOfType<SnakeBlock>();
        int snakeBlocksCount = snakeBlocks.Length;

        for (int i = 0; i < snakeBlocksCount; i++) {
            snakeBlocks[i].HideAllParts();
            if (i > 0) {
                snakeBlocks[i].ConnectTo(snakeBlocks[i - 1]);
            }
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

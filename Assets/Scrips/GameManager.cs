using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Snake")]
    public Snake snake;
    public FoodSpawner foodSpawner;
    private bool isAlive = true;

    [Header("UI")]
    public Text scoreText;
    private int score = 0;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Grid")]
    public int gridHeight = 16;
    public int gridWidth = 26;
    public float gridOffsetX = 0;
    public float gridOffsetY = 1;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "Score: 0";

        foodSpawner.Spawn(FreeSpots());
    }

    private void Update()
    {
        scoreText.text = "Score: " +  score.ToString();

        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow) && snake.CurrentDirection() != Snake.Directions.Right) {
            snake.SetDirection(Snake.Directions.Left);
        } else if (Input.GetKey(KeyCode.RightArrow) && snake.CurrentDirection() != Snake.Directions.Left) {
            snake.SetDirection(Snake.Directions.Right);
        } else if (Input.GetKey(KeyCode.DownArrow) && snake.CurrentDirection() != Snake.Directions.Up) {
            snake.SetDirection(Snake.Directions.Down);
        } else if (Input.GetKey(KeyCode.UpArrow) && snake.CurrentDirection() != Snake.Directions.Down) {
            snake.SetDirection(Snake.Directions.Up);
        } else if (!isAlive && Input.GetKey(KeyCode.Space)) {
            snake.Reset();
            score = 0;
            HideLoseScreen();
            snake.StartMoving();
        }
    }

    private void CreateSideWall(float xPosition, GameObject wallPrefab)
    {
        int start = 0 - gridHeight / 2;

        for (int i = start; i <= gridHeight / 2; i++) {
            Vector3 wallPosition = new Vector3(
                xPosition,
                i * 1 + gridOffsetY,
                0
            );

            GameObject wall = Instantiate(
                wallPrefab,
                wallPosition,
                Quaternion.identity
            ) as GameObject;
            wall.transform.parent = transform;
        }
    }

    private void CreateUpperLowerWall(float yPosition, GameObject wallPrefab)
    {
        int start = 0 - gridWidth / 2;

        for (int i = start; i <= gridWidth / 2; i++) {
            Vector3 wallPosition = new Vector3(
                i * 1 + gridOffsetX,
                yPosition,
                0
            );

            GameObject wall = Instantiate(
                wallPrefab,
                wallPosition,
                Quaternion.identity
            ) as GameObject;
            wall.transform.parent = transform;
        }
    }

    public void FoodWasEaten()
    {
        score++;

        List<Vector3> freeSpots = FreeSpots();
        if (freeSpots.Count <= 0) {
            PlayerWins();
            snake.StopMoving();
        } else {
            foodSpawner.Spawn(freeSpots);

            snake.Grow();
        }
    }

    private void PlayerWins()
    {
        winScreen.SetActive(true);
    }

    public void GameOver()
    {
        isAlive = false;
        loseScreen.SetActive(true);
        snake.StopMoving();
    }

    private void HideLoseScreen()
    {
        loseScreen.SetActive(false);
    }

    internal List<Vector3> FreeSpots()
    {
        List<SnakeBlock> snakeBlocks = Snake.SnakeBlocks();

        Vector3 lowerLeftCorner = new Vector3(
            (gridWidth / 2) * -1,
            (gridHeight / 2) * -1
        );
        Vector3 upperRightCorner = new Vector3(
            gridWidth / 2,
            gridHeight / 2
        );

        List<Vector3> freeSpots = new List<Vector3>();
        for (int y = (int)lowerLeftCorner.y + 1; y <= (int)upperRightCorner.y + 1; y++) {
            for (int x = (int)lowerLeftCorner.x; x <= (int)upperRightCorner.x; x++) {
                Boolean freeSpot = true;
                foreach (SnakeBlock snakeBlock in snakeBlocks) {
                    if (snakeBlock.transform.position.x == x && snakeBlock.transform.position.y == y) {
                        freeSpot = false;
                        break;
                    }
                }

                if (freeSpot) {
                    freeSpots.Add(
                        new Vector3(x, y)
                    );
                }
            }
        }

        return freeSpots;
    }
}

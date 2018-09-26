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
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;
    public GameObject countdownScreen;

    private int score = 0;
    private int currentResumeTime;

    private System.Timers.Timer timer;

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

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        snake.StopMoving();
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        snake.StartMoving();
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

        float yStart = lowerLeftCorner.y + gridOffsetY;
        float yEnd = upperRightCorner.y + gridOffsetY;
        float xStart = lowerLeftCorner.x + gridOffsetX;
        float xEnd = upperRightCorner.x + gridOffsetX;

        List<Vector3> freeSpots = new List<Vector3>();
        for (float y = yStart; y <= yEnd; y++) {
            for (float x = xStart; x <= xEnd; x++) {
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

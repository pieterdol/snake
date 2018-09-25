using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Snake")]
    public Snake snake;

    [Header("UI")]
    public Text scoreText;

    [Header("Grid")]
    public int gridHeight = 16;
    public int gridWidth = 26;
    public float gridOffsetX = 0;
    public float gridOffsetY = 1;

    [Header("Walls")]
    public GameObject topWallPrefab;
    public GameObject bottomWallPrefab;
    public GameObject leftWallPrefab;
    public GameObject rightWallPrefab;

    // Use this for initialization
    void Start ()
    {
        scoreText.text = "Score: 0";

        CreateWalls();
        //CreateSnake();
    }

    private void CreateWalls()
    {
        CreateUpperLowerWall(gridHeight / 2 + gridOffsetY, topWallPrefab);
        CreateUpperLowerWall((gridHeight / 2) * -1 + gridOffsetY, bottomWallPrefab);
        CreateSideWall((gridWidth / 2 + gridOffsetX) * -1 - 1, leftWallPrefab);
        CreateSideWall(gridWidth / 2 + gridOffsetX + 1, rightWallPrefab);
    }

    private void Update()
    {
        // Horizontal movement
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            snake.direction = Snake.Directions.Left;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            snake.direction = Snake.Directions.Right;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            snake.direction = Snake.Directions.Down;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            snake.direction = Snake.Directions.Up;
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
}

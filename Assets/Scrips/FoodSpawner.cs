using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Food foodPrefab;

    public void Spawn(GameManager gameManager)
    {
        Snake snake = gameManager.snake;
        Vector3 lowerLeftCorner = new Vector3(
            (gameManager.gridWidth / 2) * -1,
            (gameManager.gridHeight / 2) * -1
        );
        Vector3 upperRightCorner = new Vector3(
            gameManager.gridWidth / 2,
            gameManager.gridHeight / 2
        );

        List<Vector3> freeSpots = new List<Vector3>();
        for (int y = (int)lowerLeftCorner.y + 1; y <= (int)upperRightCorner.y + 1; y++) {
            for (int x = (int)lowerLeftCorner.x; x <= (int)upperRightCorner.x; x++) {
                freeSpots.Add(
                    new Vector3(x, y)
                );
            }
        }
        Food food = Instantiate(
            foodPrefab,
            freeSpots[new System.Random().Next(0, freeSpots.Count)],
            Quaternion.identity
        ) as Food;
    }

    //private Vector3 GetRandomFreePosition(GameManager gameManager)
    //{
    //    List<Vector3>[] freeSpots = GetFreeSpots(gameManager);

    //    return freeSpots[new System.Random().Next(0, freeSpots.Length)];
    //}

    //private List<Vector3>[] GetFreeSpots(GameManager gameManager)
    //{

    //    return freeSpots;
    //}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Food foodPrefab;

    public void Spawn(List<Vector3> freeSpots)
    {
        if (freeSpots.Count <= 0) {
            return;
        }

        Instantiate(
            foodPrefab,
            freeSpots[new System.Random().Next(0, freeSpots.Count - 1)],
            Quaternion.identity
        );
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

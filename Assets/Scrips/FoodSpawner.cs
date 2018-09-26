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
}

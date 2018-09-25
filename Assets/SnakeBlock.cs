using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBlock : MonoBehaviour
{
    Vector3 position;

    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    public SnakeBlock(Vector3 blockPosition)
    {
        position = blockPosition;
    }

    public Vector3 Position()
    {
        return position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        position = newPosition;
    }

    internal void ShowTop()
    {
        top.GetComponent<Renderer>().enabled = true;
    }
}

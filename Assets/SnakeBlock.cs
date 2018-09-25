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

    private void ShowPart(GameObject part)
    {
        part.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void HidePart(GameObject part)
    {
        part.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ConnectTo(SnakeBlock snakeBlock)
    {
        if (transform.position.y < snakeBlock.transform.position.y) {
            ShowPart(top);
        }
        if (transform.position.y > snakeBlock.transform.position.y) {
            ShowPart(bottom);
        }
        if (transform.position.x > snakeBlock.transform.position.x) {
            ShowPart(left);
        }
        if (transform.position.x < snakeBlock.transform.position.x) {
            ShowPart(right);
        }
    }

    public void HideAllParts()
    {
        HidePart(top);
        HidePart(bottom);
        HidePart(left);
        HidePart(right);
    }
}

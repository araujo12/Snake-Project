using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBody : MonoBehaviour
{
    public Vector3 bodyPos;
    public int waitUps;
   
    void Start()
    {
        bodyPos = transform.position;
    }

    
    void Update()
    {
        IncrementBody();
        
    }

    public void SetTarget(Vector3 pos)
    {
        if (waitUps > 0)
        {
            waitUps--;
            return;
        }

        bodyPos = pos;
    }

    public void WaitHeadUpdates(int value)
    {
        waitUps = value;
    }

    void IncrementBody()
    {
        transform.position = Vector3.MoveTowards(transform.position, bodyPos, PlayerControl.speed * Time.deltaTime);

    }
}

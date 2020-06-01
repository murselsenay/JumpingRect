using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimits : MonoBehaviour
{

    public float minX = -2.6f, maxX = 2.6f, minY=-5.6f;
    private bool outOfBounds;
    void Update()
    {
        CheckLimits();
    }
    void CheckLimits()
    {
        Vector2 temp = transform.position;
        if (temp.x > maxX)
            temp.x = maxX;

        if (temp.x < minX)
            temp.x = minX;

        transform.position = temp;

        if (temp.y <= minY)
        {
            if(!outOfBounds)
            {
                outOfBounds = true;

            }
        }

    }
   
}

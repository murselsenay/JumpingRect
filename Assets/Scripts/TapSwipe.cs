using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSwipe : MonoBehaviour
{
    [SerializeField]
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float angle;
    private float swipeDistanceX;
    private float swipeDistanceY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
                swipeDistanceX = Mathf.Abs((lp.x - fp.x));
                swipeDistanceY = Mathf.Abs((lp.y - fp.y));
            }
            if (touch.phase == TouchPhase.Ended)
            {
                angle = Mathf.Atan2((lp.x - fp.x), (lp.y - fp.y)) * 57.2957795f;

                if (angle > 60 && angle < 120 && swipeDistanceX > 40)
                {
                    Debug.Log("right swipe...");
                }
                if (angle > 150 || angle < -150 && swipeDistanceY > 40)
                {
                    Debug.Log("down  swipe...");
                }
                if (angle < -60 && angle > -120 && swipeDistanceX > 40)
                {
                    Debug.Log("left  swipe...");
                }
                if (angle > -30 && angle < 30 && swipeDistanceY > 40)
                {
                    Debug.Log("up  swipe...");
                }
            }
        }
    }

}


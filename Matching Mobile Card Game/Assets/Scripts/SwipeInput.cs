using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private float swipeThreshold = 150f; 

    void Update()
    {
       
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }

        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
        }
#endif
    }

    void DetectSwipe()
    {
        float verticalDistance = endTouchPosition.y - startTouchPosition.y;

       
        if (verticalDistance < -swipeThreshold)
        {
            Debug.Log("Swipe Down Detected - Restarting Level");
            GameManager.Instance.RestartLevel();
        }
    }
}

using UnityEngine;

public class TwoFingerTapInput : MonoBehaviour
{
    private float maxTapTime = 0.3f; 
    private float[] touchStartTime = new float[2];

    void Update()
    {
        // Detect exactly two touches
        if (Input.touchCount == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                    touchStartTime[i] = Time.time;

                if (t.phase == TouchPhase.Ended)
                {
                    float duration = Time.time - touchStartTime[i];
                    if (duration > maxTapTime) return; 
                }
            }

            
            if (Input.GetTouch(0).phase == TouchPhase.Ended &&
                Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                GameManager.Instance.TogglePause();
            }
        }

        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            
            GameManager.Instance.TogglePause();
        }
#endif
    }
}

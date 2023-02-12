using UnityEngine;

public class MobileTouchTypeInput
{     
    public enum TouchType
    {
        SHORT, LONG, NONE
    }

    [SerializeField]
    private static float timeDelayThreshold = 0.3f;
    private static float timeTouchBegan = 0.0f;
    private static float timeTouchEnd = 0.0f;

    public static TouchType GetTouchType()
    {
        if (Input.touchCount < 1) return TouchType.NONE;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        { // If the user puts her finger on screen...
            timeTouchBegan = Time.time;
            Debug.Log($"timeTouchBegan: {timeTouchBegan}");
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        { // If the user raises her finger from screen
            timeTouchEnd = Time.time;
            var diff = timeTouchEnd - timeTouchBegan;
            Debug.Log($"timeTouchEnd: {timeTouchEnd}, diff: {diff}, threshold: {timeDelayThreshold}, greater then threshold: {diff > timeDelayThreshold}");
            if (diff > timeDelayThreshold)
            { // Is the time pressed greater than our time delay threshold?
                return TouchType.LONG;
            }
            else
            {
                return TouchType.SHORT;
            }
        }
        return TouchType.NONE;
    }
}

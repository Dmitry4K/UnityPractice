using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    float angle = 0.0f;
    [SerializeField]
    float sensitivity = 0.05f;
    float prevMoved = -1.0f;
    float currMoved = -1.0f;
    float endForce = 0.0f;
    readonly float forceDelta = 0.05f;
    readonly float forceCoeff = 1.2f;
    readonly float forceStep = 35.0f;
    Camera playerCamera;
    float radius = 0.0f;
    [SerializeField]
    private RectTransform ignoreArea;


    void Start()
    {
        playerCamera = GetComponent<Camera>();
        radius = playerCamera.transform.position.magnitude;
    }

    void Update()
    {
        if (Input.touchCount > 0 && !IsOnSliders()) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) { prevMoved = currMoved = Input.mousePosition.x; return; }
            if (Input.GetTouch(0).phase == TouchPhase.Ended) { endForce = prevMoved - currMoved; prevMoved = -1.0f; currMoved = -1.0f; return; }
            if (Input.GetTouch(0).phase == TouchPhase.Stationary) { return; }
            if (Input.GetTouch(0).phase == TouchPhase.Moved) { prevMoved = currMoved; currMoved = Input.mousePosition.x; }
            if (prevMoved < 0.0f) { return; }  
            if (currMoved < 0.0f) { return; }
        }
        CalcNewCameraTransform();
    }

    private void CalcAngleEndForce()
    {
        angle += sensitivity * (prevMoved - currMoved + endForce);

        if (Mathf.Abs(endForce) > forceStep)
        {
            endForce /= forceCoeff;
        }
        else
        {
            if (endForce > 0.0f)
            {
                endForce = Mathf.Max(0.0f, endForce - forceDelta);
            }
            else
            {
                endForce = Mathf.Min(0.0f, endForce + forceDelta);
            }
        }

        angle %= 360.0f;
    }

    private void CalcPosition()
    {
        float radian = Mathf.PI * angle / 180.0f;
        playerCamera.transform.position = new Vector3(
            -radius * Mathf.Sin(radian),
            playerCamera.transform.position.y,
            -radius * Mathf.Cos(radian)
        );
    }

    private void CalcNewCameraTransform()
    {
        if (Input.touchCount > 0 || Mathf.Abs(endForce) > 0.01f)
        {
            CalcAngleEndForce();
            CalcPosition();
            Quaternion targetRotation = Quaternion.Euler(25.0f, angle, 0);
            playerCamera.transform.rotation = targetRotation;
        }
    }

    private bool IsOnSliders()
    {
        Vector3 t = Input.mousePosition;
        float yd = Screen.height - ignoreArea.position.y - ignoreArea.rect.height / 2.0f;
        float xd = ignoreArea.position.x - ignoreArea.rect.width / 2.0f;
        return (t.x < ignoreArea.rect.width + xd && t.y > Screen.height - ignoreArea.rect.height - yd);
    }
}
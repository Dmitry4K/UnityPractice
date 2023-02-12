using System.Collections.Generic;
using UnityEngine;

public class MouseClicking : MonoBehaviour
{
    [SerializeField]
    private float forceStrength = 1.0f;

    [SerializeField]
    private float randomCoeff = 0.0f;

    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Color touchedColor = Color.yellow;

    [SerializeField]
    private float timeDelayThreshold = 0.3f;

    private Color baseColor;

    private readonly List<GameObject> gameObjects = new();

    void Update()
    {
        Vector3 hitPoint;
        var touch = MobileTouchTypeInput.GetTouchType();
        if (touch == MobileTouchTypeInput.TouchType.SHORT) // Left Button
        {
            var clickedGameObj = ClickToObj(out hitPoint);
            if (clickedGameObj.CompareTag("Ball"))
            {
                SetColor(clickedGameObj, touchedColor);
                gameObjects.Add(clickedGameObj);
            }
            else if (clickedGameObj.CompareTag("Floor"))
            {
                baseColor = Instantiate(obj, hitPoint + (new Vector3(0.0f, 0.5f, 0.0f)), Quaternion.identity)
                    .GetComponent<Renderer>()
                    .material
                    .color;
            }
        }
        if (touch == MobileTouchTypeInput.TouchType.LONG) // Right Button
        {
            ClickToObj(out hitPoint);
            foreach (GameObject go in gameObjects)
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                var forceVector = GetForceVector(rb.transform.position, hitPoint);
                rb.AddForce(forceVector, ForceMode.Impulse);
                SetColor(go, (Color)baseColor);
            }
            gameObjects.Clear();
        }
    }

    private Vector3 GetForceVector(Vector3 objectPosition, Vector3 forcePoint)
    {

        Vector3 forceVector = (forcePoint - objectPosition).normalized;
        float randomMult = Random.Range(0.0f, 1.0f);
        return forceVector * (forceStrength + randomCoeff * randomMult);
    }

    private GameObject ClickToObj(out Vector3 hitPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            hitPoint = hit.point;
            return hit.transform.gameObject;
        }
        hitPoint = Vector3.zero;
        return null;
    }

    private void SetColor(GameObject obj, Color color)
    {
        if (obj.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.SetColor("_Color", color);
        }
    }
}

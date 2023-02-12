using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TouchScript : MonoBehaviour
{
    [SerializeField]
    private Color touchedColor = Color.yellow;
    [SerializeField]
    private float forceStrength = 1.0f;
    [SerializeField]
    private float randomCoeff = 0.02f;
    [SerializeField]
    private Button pushButton;
    [SerializeField]
    private GameObject pointObj;
    private Camera playerCamera;
    private readonly int BALLS_LAYER = 1 << 7;
    private readonly List<GameObject> gameObjects = new();
    private Color? baseColor = null;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        pushButton.onClick.AddListener(delegate () { Click(); });
    }


    void Update()
    {
        if (MobileTouchTypeInput.GetTouchType() == MobileTouchTypeInput.TouchType.SHORT)
        {
            GameObject hitedBall = ClickToObj(out _, BALLS_LAYER);
            if (hitedBall != null)
            {
                InitBaseColor(hitedBall);
                SetColor(hitedBall, touchedColor);
                gameObjects.Add(hitedBall);
            }
        }
    }
    private GameObject ClickToObj(out Vector3 hitPoint, int layerMask)
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
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
    private Vector3 GetForceVector(Vector3 objectPosition, Vector3 forcePoint)
    {

        Vector3 forceVector = (forcePoint - objectPosition).normalized;
        float randomMult = Random.Range(0.0f, 1.0f);
        return forceVector * (forceStrength + randomCoeff * randomMult);
    }

    private void Click()
    {
        Vector3 pos = new(pointObj.transform.position.x, pointObj.transform.position.y, pointObj.transform.position.z);
        foreach (GameObject go in gameObjects)
        {
            Rigidbody rb = go.GetComponent<Rigidbody>();
            var forceVector = GetForceVector(rb.transform.position, pos);
            rb.AddForce(forceVector, ForceMode.Impulse);
            SetColor(go, (Color)baseColor);
        }
        gameObjects.Clear();
    }

    private void InitBaseColor(GameObject obj)
    {
        if (baseColor == null)
        {
            Color color = obj.GetComponent<Renderer>().material.color;
            baseColor = new Color(color.r, color.g, color.b, color.r);
        }
    }
}

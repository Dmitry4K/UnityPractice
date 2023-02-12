using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonScript : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject pointObj;
    [SerializeField]
    private GameObject objToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = objToSpawn.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        button.onClick.AddListener(delegate () { click(); });
    }

    private void click()
    {
        Vector3 position = new Vector3(
            pointObj.transform.position.x + GetRandomValue(),
            pointObj.transform.position.y + GetRandomValue(),
            pointObj.transform.position.z + GetRandomValue()
        );
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        Instantiate(objToSpawn, position, rotation);
    }

    private float GetRandomValue()
    {
        return Random.Range(-0.2f, 0.2f);
    }
}

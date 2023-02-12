using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private Slider sliderX;
    [SerializeField]
    private Slider sliderY;
    [SerializeField]
    private Slider sliderZ;
    [SerializeField]
    private GameObject pointToSpawn;
    [SerializeField]
    private GameObject cube;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float posX = cube.transform.position.x + sliderX.value * cube.GetComponent<Renderer>().bounds.size.x * 0.9f / 2.0f;
        float posY = cube.transform.position.y + sliderY.value * cube.GetComponent<Renderer>().bounds.size.y * 0.9f / 2.0f;
        float posZ = cube.transform.position.z + sliderZ.value * cube.GetComponent<Renderer>().bounds.size.z * 0.9f / 2.0f;
        pointToSpawn.transform.position = new Vector3(posX, posY, posZ);
    }
}

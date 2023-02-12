using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
	private float border = 5.5f;
	
	[SerializeField]
	private GameObject obj;
	float RandX;
	float RandZ;
	Vector3 whereToSpawn;
	[SerializeField]
	private float spawnRate = 2f;
	float nextSpawn = 0.0f;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
			whereToSpawn = getPosition();
			Instantiate(obj, whereToSpawn, Quaternion.identity);
		}
	}
	
	private Vector3 getPosition() {
		RandX = Random.Range(-border, border);
		RandZ = Random.Range(-border, border);
		float RandY = 1.0f; // Random.Range(1.0f, 20.0f);
		return new Vector3(RandX, RandY, RandZ);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerVisualSpawner : MonoBehaviour {

	public int flowerCount = 100;
	public GameObject[] flowerPrefabs;

	MeshCollider meshCollider;

	void Start () {
		meshCollider = GetComponent<MeshCollider>();

		for (int i = 0; i < flowerCount; i++)
		{
			Vector3 randomViewportPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f);
			Ray ray = Camera.main.ViewportPointToRay(randomViewportPoint);
			RaycastHit raycastHit;

			int attempts = 0;
			while (meshCollider.Raycast(ray, out raycastHit, 100.0f)) {
				attempts++;

				if (attempts > 100) break;
			}

			Instantiate(flowerPrefabs[Random.Range(0, flowerPrefabs.Length)], raycastHit.point, Quaternion.identity, this.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

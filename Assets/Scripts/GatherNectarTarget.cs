using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherNectarTarget : Target
{

	public GameObject[] flowers;
	public int minFlowersToSpawn = 3;
	public int maxFlowersToSpawn = 6;
	public float spawnSpread = 1.0f;

	protected override void Start()
	{
		base.Start();

		int flowersToSpawn = Random.Range(minFlowersToSpawn, maxFlowersToSpawn);

		for (int i = 0; i < flowersToSpawn; i++)
		{
			Vector3 spawnPosition = Random.insideUnitCircle * spawnSpread;
			Instantiate(flowers[Random.Range(0, flowers.Length - 1)], spawnPosition, Quaternion.identity, this.transform);
		}
	}

}

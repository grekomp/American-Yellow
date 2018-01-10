using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float maxInterval = 20;
	public float minInterval = 5;
	public float currentInterval;

	public float randomness = 2;
	public float difficultyIncreaseRatio = 0.2f;

	public float spawnRange = 20;

	public GameObject spawnPrefab;

	public float timer;

	public static EnemySpawner instance;

	void Start () {
		instance = this;
		currentInterval = maxInterval;
		timer = GetNextSpawnInterval();
	}
	
	void Update () {
		timer -= Time.deltaTime;

		if(currentInterval >= minInterval)
		{
			currentInterval -= difficultyIncreaseRatio * Time.deltaTime;
		}

		if(timer <= 0)
		{
			timer = GetNextSpawnInterval();
			Spawn();
		}

	}

	float GetNextSpawnInterval()
	{
		return currentInterval - Random.Range(-1.0f, 1.0f) * randomness;

	}

	Vector3 GetNextSpawnPosition()
	{
		return Random.insideUnitCircle.normalized * spawnRange;
	}

	void Spawn()
	{
		Instantiate(spawnPrefab, GetNextSpawnPosition(), Quaternion.identity, FolderHelper.instance.enemies);

	}
}

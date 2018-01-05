using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

	public float minInterval = 1.0f;
	public float maxInterval = 10.0f;

	public int alive = 0;
	public int maxAlive = 3;

	public Target spawnPrefab;

	public Vector2 bottomLeftCorner = new Vector2(-8,-4);
	public Vector2 topRightCorner = new Vector2(8, 4);

	public float timer;

	public static TargetSpawner instance;

	void Start () {
		instance = this;

		timer = GetNextSpawnInterval();
	}
	
	void Update () {
		if (alive < maxAlive)
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				timer = GetNextSpawnInterval();
				Spawn();
			}
		}
	}

	float GetNextSpawnInterval()
	{
		return Random.Range(minInterval, maxInterval);
	}

	Vector3 GetNextSpawnPosition()
	{
		return new Vector3(Random.Range(bottomLeftCorner.x, topRightCorner.x), Random.Range(bottomLeftCorner.y, topRightCorner.y));
	}

	void Spawn()
	{
		alive++;

		Target spawnedObject = Instantiate(spawnPrefab, GetNextSpawnPosition(), Quaternion.identity, FolderHelper.instance.targets) as Target;

		spawnedObject.sourceSpawner = this;
	}

	public void Remove()
	{
		alive--;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour {

	public Hive hive;
	public int maxBees = 20;
	public int numBees = 20;

	public float attackDamage = 1;

	public float targetReachedTolerance = 0.5f;

	// Swarm parameters
	Bee[] bees;
	public Bee beePrefab;

	public float selfLearning = 2.0f;
	public float globalLearning = 2.0f;

	public float randomizationMultiplier = 1.0f;
	public float maxBeesVelocity = 5.0f;

	void Start()
	{
		Initialize();
		hive = GameManager.instance.hive;
		GameManager.instance.AddedEnemy();

	}

	void Update()
	{
		if (hive != null)
		{
			if (IsTargetReached())
			{
				AttackHive();
			}
			else
			{
				CalculateUnitPosition();
			}
		}
	
		UpdateBeesVelocity(transform);
		
	}
	

	private void AttackHive()
	{
		hive.Damage(attackDamage * Time.deltaTime);
	}

	private bool IsTargetReached()
	{
		return (hive.transform.position - transform.position).magnitude <= targetReachedTolerance;
	}

	private void CalculateUnitPosition()
	{
		transform.position = Vector3.Lerp(transform.position, hive.transform.position, Time.deltaTime);
	}

	private void UpdateBeesVelocity(Transform targetTransform)
	{
		Bee bestBee = null;
		float bestFitness = float.MinValue;

		foreach (Bee bee in bees)
		{
			float currentFitness = Fitness(bee.transform.position, targetTransform.position);
			if (currentFitness > Fitness(bee.bestPosition, targetTransform.position))
			{
				bee.bestPosition = bee.transform.position;
			}

			if (currentFitness > bestFitness)
			{
				bestFitness = currentFitness;
				bestBee = bee;
			}
		}

		if (bestBee != null)
		{
			foreach (Bee bee in bees)
			{
				bee.velocity += Time.deltaTime * (selfLearning * UnityEngine.Random.value * (bee.bestPosition - bee.transform.position) + globalLearning * UnityEngine.Random.value * (bestBee.transform.position - bee.transform.position));

				Vector2 rand = UnityEngine.Random.insideUnitCircle;
				bee.velocity += new Vector3(rand.x, rand.y, 0.0f) * Time.deltaTime * randomizationMultiplier;

				if (bee.velocity.magnitude > maxBeesVelocity)
					bee.velocity = (bee.velocity / bee.velocity.magnitude) * maxBeesVelocity;
			}
		}
	}

	float Fitness(Vector3 currentPosition, Vector3 targetPosition)
	{
		return -(currentPosition - targetPosition).magnitude;
	}

	void Initialize()
	{
		bees = new Bee[maxBees];

		for (int i = 0; i < maxBees; i++)
		{
			bees[i] = Instantiate(beePrefab, transform.position, transform.rotation, FolderHelper.instance.bees).GetComponent<Bee>();
			bees[i].RandomizeVelocity();
		}
	}

	public void KillBees()
	{
		foreach(Bee bee in bees)
		{
			if(bee != null)
			{
				Debug.Log("Destroying bee");
				Destroy(bee.gameObject);
			}
		}
	}

	void OnDestroy()
	{
		Debug.Log("Destroying enemy unit");
		KillBees();
		GameManager.instance.DestroyedEnemy();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeUnit : MonoBehaviour {

	public int numBees = 20;
	public Target target;
	public Bee[] bees;

	public Bee beePrefab;

	public float selfLearning = 2.0f;
	public float globalLearning = 2.0f;

	public float randomizationMultiplier = 1.0f;
	public float maxVelocity = 1.0f;

	// Use this for initialization
	void Start () {
		Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateBeesVelocity();
		CalculateUnitPosition();
	}

	private void CalculateUnitPosition()
	{
		Vector3 averageBeesPosition = new Vector3();

		foreach (Bee bee in bees)
		{
			averageBeesPosition += bee.transform.position;
		}

		averageBeesPosition /= bees.Length;

		transform.position = Vector3.Lerp(transform.position, averageBeesPosition, Time.deltaTime);
	}

	private void UpdateBeesVelocity()
	{
		Bee bestBee = null;
		float bestFitness = float.MinValue;

		foreach (Bee bee in bees)
		{
			float currentFitness = Fitness(bee.transform.position, target.transform.position);
			if (currentFitness > Fitness(bee.bestPosition, target.transform.position))
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

				if (bee.velocity.magnitude > maxVelocity)
					bee.velocity = (bee.velocity / bee.velocity.magnitude) * maxVelocity;
			}
		}
	}

	float Fitness(Vector3 currentPosition, Vector3 targetPosition)
	{
		return -(currentPosition - targetPosition).magnitude;
	}

	public void Initialize()
	{
		bees = new Bee[numBees];

		for (int i = 0; i < numBees; i++)
		{
			bees[i] = Instantiate(beePrefab, Vector3.zero, transform.rotation).GetComponent<Bee>();
			bees[i].RandomizeVelocity();
		}
	}
}

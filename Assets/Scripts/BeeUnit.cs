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

	// Use this for initialization
	void Start () {
		Initialize();
	}

	// Update is called once per frame
	void Update() {
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
			foreach(Bee bee in bees)
			{
				bee.velocity += Time.deltaTime * (selfLearning * Random.value * (bee.bestPosition - bee.transform.position) + globalLearning * Random.value * (bestBee.transform.position - bee.transform.position));

				Vector2 rand = Random.insideUnitCircle;
				bee.velocity += new Vector3(rand.x, rand.y, 0.0f) * Time.deltaTime * randomizationMultiplier;
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
			bees[i] = Instantiate(beePrefab, transform.position, transform.rotation).GetComponent<Bee>();
			bees[i].RandomizeVelocity();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeUnit : MonoBehaviour {

	public Target target;

	public int maxBees = 20;
	public int numBees = 20;

	public float targetReachedTolerance = 0.1f;

	public Color unitColor;

	// Actions
	public bool carriesInventory { get { return carriedItem != null; } }
	public InventoryItem carriedItem;

	// Swarm parameters
	Bee[] bees;
	public Bee beePrefab;

	public float selfLearning = 2.0f;
	public float globalLearning = 2.0f;

	public float randomizationMultiplier = 1.0f;
	public float maxBeesVelocity = 5.0f;

	void Start () {
		Initialize();
		GameManager.instance.AddedUnit();

		//unitColor = UnityEngine.Random.ColorHSV();
		UIManager.instance.AddUnitIcon(this);
	}

	void Update()
	{
		if (target != null)
		{
			if (IsTargetReached() && target.actionStarted == false)
			{
				TryStartTargetAction();
			}

			UpdateBeesVelocity(target.transform);
			CalculateUnitPosition();
		} else
		{
			UpdateBeesVelocity(transform);
		}
		
	}

	private bool IsTargetReached()
	{
		return (target.transform.position - transform.position).magnitude <= targetReachedTolerance;
	}

	private void CalculateUnitPosition()
	{
		transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
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

	public void TryStartTargetAction()
	{
		target.StartAction();
	}

	public bool CarriesItemOfType(ItemTypes type)
	{
		if (type == ItemTypes.None && carriesInventory == false)
			return true;

		if (carriesInventory && type == carriedItem.type)
			return true;

		return false;
	}

	public bool ChangeTarget(Target newTarget)
	{
		if ((target == null || target.CanLeave()) && newTarget.CanBeOccupiedBy(this))
		{
			if (target != null) target.TryLeave();
			newTarget.Occupy(this);

			target = newTarget;
			return true;
		} else
		{
			return false;
		}
	}

	public void KillBees(int amount)
	{
		if (numBees >= amount)
		{
			numBees -= amount;
		} else {
			numBees = 0;
		}

		for (int i = numBees; i < maxBees; i++)
		{
			if (bees[i] != null)
			{
				Destroy(bees[i]);
			}
		}

		if (numBees == 0)
		{
			DestroyUnit();
		}
	}

	void DestroyBees()
	{
		for (int i = 0; i < maxBees; i++)
		{
			if (bees[i] != null)
			{
				Destroy(bees[i]);
			}
		}
	}

	private void DestroyUnit()
	{
		GameManager.instance.DestroyedUnit();
		DestroyBees();
		Destroy(gameObject);
	}

	public void SpawnBees(int amount)
	{
		numBees += amount;

		if (numBees > maxBees) numBees = maxBees;

		for (int i = 0; i < maxBees; i++)
		{
			if (bees[i] == null)
			{
				bees[i] = Instantiate(beePrefab, transform.position, transform.rotation, FolderHelper.instance.bees).GetComponent<Bee>();
				bees[i].RandomizeVelocity();
			}
		}

	}
}

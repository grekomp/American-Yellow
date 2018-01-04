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
	public bool actionInProgress = false;
	public GameAction currentAction;

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
		UIManager.AddUnitIcon(this);
	}

	void Update()
	{
		if(IsTargetReached())
		{
			if (actionInProgress == false)
			{
				StartAction(target.GetAction());
			}
		}

		UpdateActionProgress();
		UpdateBeesVelocity();
		CalculateUnitPosition();
	}

	private bool IsTargetReached()
	{
		return (target.transform.position - transform.position).magnitude <= targetReachedTolerance;
	}

	private void UpdateActionProgress()
	{
		if (actionInProgress)
		{
			if (currentAction.actionFinished)
			{
				if (currentAction.requiresInventorySpace)
				{
					carriedItem = currentAction.GetReward();
				}
			}
		}
	}

	private void CalculateUnitPosition()
	{
		transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
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
			bees[i] = Instantiate(beePrefab, transform.position, transform.rotation).GetComponent<Bee>();
			bees[i].RandomizeVelocity();
		}
	}

	public void StartAction(GameAction action)
	{
		if (
			actionInProgress == false &&
			action.actionInProgress == false &&
			(action.requiresInventorySpace == false || carriesInventory == false)
			)
		{
			currentAction = action;
			actionInProgress = true;

			action.StartAction();
		}
	}
}

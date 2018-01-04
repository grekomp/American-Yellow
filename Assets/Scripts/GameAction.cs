using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction : MonoBehaviour {

	public float actionTimeFull = 2.0f;
	public float actionTimeLeft = 2.0f;
	public bool actionInProgress = false;
	public bool actionFinished = false;
	public bool requiresInventorySpace { get { return reward != null; } }

	public InventoryItem reward;

	void Start () {
		
	}
	
	void Update () {
		if (actionInProgress)
		{
			actionTimeLeft -= Time.deltaTime;

			if (actionTimeLeft <= 0)
			{
				actionFinished = true;
				actionInProgress = false;
			}
		}
	}

	public virtual void StartAction()
	{
		actionInProgress = true;
	}

	public virtual InventoryItem GetReward()
	{
		return reward;
	}

	public virtual void EndAction()
	{
		Destroy(gameObject);
	}
}

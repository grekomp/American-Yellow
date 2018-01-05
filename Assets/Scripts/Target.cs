using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public bool isOccupied { get { return occupant != null; } }
	public BeeUnit occupant;

	public bool actionStarted = false;
	public bool actionFinished = false;

	public float actionTimeFull = 4.0f;
	public float actionTimeRemaining;

	public bool isInteruptible = false;
	public bool resetOnInterupt = true;

	public bool destroyOnComplete = false;

	public InventoryItem reward;
	public bool requiresFreeInventory = false;

	public ItemTypes requiredItem = ItemTypes.None;
	public bool consumesItem = true;

	public Sprite icon;
	public Color iconBackgroundColor;

	public TargetSpawner sourceSpawner;

	protected virtual void Start () {
		UIManager.instance.AddTargetIcon(this);

		Reset();
	}
	
	protected virtual void Update () {

		if (actionStarted && actionFinished == false)
		{
			actionTimeRemaining -= Time.deltaTime;

			if (actionTimeRemaining <= 0)
			{
				FinishAction();
			}
		}
		
	}

	protected virtual void FinishAction()
	{
		actionFinished = true;

		OnActionFinished();

		if (destroyOnComplete)
		{
			if (sourceSpawner != null)
			{
				sourceSpawner.Remove();
			}

			Destroy(gameObject);
		}
	}

	protected virtual void OnActionFinished()
	{
		if (reward != null)
		{
			occupant.carriedItem = reward;
		}
	}

	public virtual bool CanBeOccupiedBy(BeeUnit unit)
	{
		if (isOccupied == false && 
			(requiredItem == ItemTypes.None || unit.CarriesItemOfType(requiredItem)) && 
			(requiresFreeInventory == false || unit.carriesInventory != requiresFreeInventory))
		{
			return true;
		}
		else
		{
			return false;
		}

	}

	public virtual bool Occupy(BeeUnit unit)
	{
		if (CanBeOccupiedBy(unit))
		{
			occupant = unit;

			return true;
		}

		return false;
	}

	public virtual void StartAction()
	{
		actionStarted = true;
		actionFinished = false;

		if (consumesItem)
		{
			occupant.carriedItem = null;
		}
	}

	public virtual bool CanLeave()
	{
		if (actionFinished || isInteruptible || actionStarted == false)
		{
			return true;
		}

		return false;
	}

	protected void Reset()
	{
		actionFinished = false;
		actionStarted = false;

		actionTimeRemaining = actionTimeFull;
	}

	public virtual bool TryLeave()
	{
		if (CanLeave())
		{
			occupant = null;

			if (actionFinished == false)
			{
				if (resetOnInterupt)
				{
					Reset();
				}
			} else
			{
				if (destroyOnComplete == false)
				{
					Reset();
				}
			}

			return true;
		}

		return false;
	}
}

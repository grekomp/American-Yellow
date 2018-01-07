using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBeeUnitTarget : Target {

	public BeeUnit unit;

	public override bool CanBeOccupiedBy(BeeUnit unit)
	{
		return base.CanBeOccupiedBy(unit) && GameManager.instance.honey >= GameManager.instance.unitProductionCost && GameManager.instance.currentUnits < GameManager.instance.maxUnits;
	}

	protected override void OnActionFinished()
	{
		base.OnActionFinished();

		GameManager.instance.TryUseHoney(GameManager.instance.unitProductionCost);

		Vector3 spawnPosition = Random.insideUnitCircle;

		Instantiate(unit, spawnPosition, Quaternion.identity, FolderHelper.instance.beeUnits);
	}
}

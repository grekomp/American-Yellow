using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairHiveTarget : Target {

	public float addedHealth = 2.0f;

	public override bool CanBeOccupiedBy(BeeUnit unit)
	{
		return base.CanBeOccupiedBy(unit) && GameManager.instance.hiveHealth < GameManager.instance.maxHiveHealth;
	}

	protected override void OnActionFinished()
	{
		base.OnActionFinished();

		GameManager.instance.RepairHive(addedHealth);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHoneyTarget : Target {

	protected override void Start () {
		base.Start();


	}
	
	protected override void Update () {
		base.Update();
	}

	protected override void OnActionFinished()
	{
		base.OnActionFinished();

		GameManager.instance.StoreHoney(1);
	}
}

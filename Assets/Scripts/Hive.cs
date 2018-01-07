using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour {

	internal void Damage(float damage)
	{
		GameManager.instance.DamageHive(damage);
	}
}

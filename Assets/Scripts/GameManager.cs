using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int honey = 0;
	public int nectar = 0;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StoreHoney(int amount)
	{
		honey += amount;

		UIManager.instance.UpdateScore(honey);
	}

	public bool TryUseHoney(int amount)
	{
		if (honey >= amount)
		{
			honey -= amount;

			UIManager.instance.UpdateScore(honey);
			return true;
		}

		return false;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public Hive hive;

	public int honey = 0;
	public int nectar = 0;

	public float hiveHealth = 10.0f;
	public float maxHiveHealth = 10.0f;

	public int maxUnits = 3;
	public int currentUnits = 0;

	public int unitProductionCost = 5;

	public int currentEnemies = 0;
	public int maxEnemies = 3; 

	void Awake () {
		instance = this;

		hiveHealth = maxHiveHealth;
	}

	private void Start()
	{
		UIManager.instance.UpdateScore(honey);
		UIManager.instance.UpdateHiveHealth(hiveHealth / maxHiveHealth);
	}

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

	public void DamageHive(float damage)
	{
		hiveHealth -= damage;

		if (hiveHealth <= 0)
		{
			GameOver();
		}

		UIManager.instance.UpdateHiveHealth(hiveHealth / maxHiveHealth);
	}

	private void GameOver()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void RepairHive(float amount)
	{
		hiveHealth += amount;
		if (hiveHealth > maxHiveHealth) hiveHealth = maxHiveHealth;

		UIManager.instance.UpdateHiveHealth(hiveHealth / maxHiveHealth);
	}

	public void DestroyedUnit()
	{
		currentUnits--;
	}

	public void AddedUnit()
	{
		currentUnits++;
	}

	public void DestroyedEnemy()
	{
		currentEnemies--;
	}

	public void AddedEnemy()
	{
		currentEnemies++;
	}

}

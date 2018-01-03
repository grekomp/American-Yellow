﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 bestPosition;

	// Use this for initialization
	void Start () {
		bestPosition = transform.position;
		RandomizeVelocity();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + velocity * Time.deltaTime;
	}

	public void RandomizeVelocity()
	{
		velocity = Random.insideUnitCircle;
	}
}

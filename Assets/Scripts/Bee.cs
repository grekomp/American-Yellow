using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 bestPosition;

	public float maxVelocity = 1.0f;

	// Use this for initialization
	void Start () {
		bestPosition = transform.position;
		RandomizeVelocity();
	}
	
	// Update is called once per frame
	void Update () {
		if (velocity.magnitude > maxVelocity)
			velocity = (velocity / velocity.magnitude) * maxVelocity;

		transform.position = transform.position + velocity * Time.deltaTime;
	}

	public void RandomizeVelocity()
	{
		velocity = Random.insideUnitCircle;
	}
}

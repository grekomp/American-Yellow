using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerWindMovement : MonoBehaviour {

	public float speed = 2.0f;
	public float magnitude = 10.0f;

	public float timeRandomness = 0.1f;

	float positionXMultiplier = 1.0f;
	float positionYMultiplier = 2.0f;

	float timeElapsed = 0;
	float timeOffset;

	void Start () {
		timeOffset = Random.Range(0, timeRandomness);
		timeOffset += transform.position.x * positionXMultiplier;
		timeOffset += transform.position.y * positionYMultiplier;
	}
	
	void Update () {
		timeElapsed += Time.deltaTime;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Cos(timeElapsed * speed + timeOffset) * magnitude));


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollower : MonoBehaviour {

	public GameObject target;

	RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	void Update () {
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(target.transform.position);

		Canvas parentCanvas = GetComponentInParent<Canvas>();

		viewportPoint.x *= parentCanvas.GetComponent<RectTransform>().rect.width;
		viewportPoint.y *= parentCanvas.GetComponent<RectTransform>().rect.height;
		viewportPoint.z = 0.0f;

		rectTransform.anchoredPosition = viewportPoint;
	}
}

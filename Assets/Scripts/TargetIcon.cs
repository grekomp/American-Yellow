using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour {

	public Target target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick()
	{
		
		SelectionManager.GetSelection().target = target;
	}
}

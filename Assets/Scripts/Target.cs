using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameAction action;

	void Start () {
		UIManager.AddTargetIcon(this);
	}
	
	void Update () {
		
	}

	public GameAction GetAction()
	{
		return action;
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderHelper : MonoBehaviour {

	public Transform unitIcons;
	public Transform targetIcons;
	public Transform gameActions;
	public Transform bees;
	public Transform beeUnits;

	public static FolderHelper instance;

	void Awake () {
		instance = this;
	}
	
	void Update () {
		
	}
}
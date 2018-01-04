using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

	static BeeUnit selection;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public static BeeUnit GetSelection()
	{
		return selection;
	}

	public static void SetSelection(BeeUnit selection)
	{
		SelectionManager.selection = selection;
	}

	public static bool IsSelected(BeeUnit beeUnit)
	{
		return selection == beeUnit;
	}
}

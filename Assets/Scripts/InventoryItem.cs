using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTypes
{
	None,
	Nectar,
	Honey,

}

public class InventoryItem : MonoBehaviour {

	public Sprite icon;
	public Color iconBackgroundColor;

	public ItemTypes type;

	void Start () {
		
	}

	void Update () {
		
	}
}

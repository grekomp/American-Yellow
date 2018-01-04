using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour {

	public BeeUnit unit;

	public GameObject selection;
	public Transform itemIcon;
	public Image itemIconImage;
	public Image itemIconBackground;
	public Slider actionSlider;

	void Start () {
		transform.Find("UnitIconBackground").GetComponent<Image>().color = unit.unitColor;
		actionSlider = transform.Find("ActionSlider").GetComponent<Slider>();
		itemIcon = transform.Find("ItemIcon");
		itemIconImage = itemIcon.Find("ItemIconImage").GetComponent<Image>();
		itemIconBackground = itemIcon.Find("ItemIconBackground").GetComponent<Image>();
	}
	
	void Update () {
		// Draw selection border
		selection.SetActive(SelectionManager.IsSelected(unit));

		// Draw action progress bar
		if (unit.target != null)
		{
			actionSlider.gameObject.SetActive(unit.target.actionStarted && unit.target.actionFinished == false);
			actionSlider.value = 1 - unit.target.actionTimeRemaining / unit.target.actionTimeFull;
		} else
		{
			actionSlider.gameObject.SetActive(false);
		}

		// Draw carried item
		itemIcon.gameObject.SetActive(unit.carriesInventory);

		if (unit.carriesInventory)
		{
			itemIconImage.sprite = unit.carriedItem.icon;
			itemIconBackground.color = unit.carriedItem.iconBackgroundColor;
		}
	}

	public void OnClick()
	{
		SelectionManager.SetSelection(unit);
	}
}

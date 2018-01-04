using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour {

	public BeeUnit target;

	public GameObject selection;
	public Transform itemIcon;
	public Image itemIconImage;
	public Image itemIconBackground;
	public Slider actionSlider;

	void Start () {
		transform.Find("UnitIconBackground").GetComponent<Image>().color = target.unitColor;
		actionSlider = transform.Find("ActionSlider").GetComponent<Slider>();
		itemIcon = transform.Find("ItemIcon");
		itemIconImage = itemIcon.Find("ItemIconImage").GetComponent<Image>();
		itemIconBackground = itemIcon.Find("ItemIconBackground").GetComponent<Image>();
	}
	
	void Update () {
		// Draw selection border
		selection.SetActive(SelectionManager.IsSelected(target));

		// Draw action progress bar
		if (target.currentAction != null)
		{
			actionSlider.gameObject.SetActive(target.currentAction.actionInProgress);
			actionSlider.value = 1 - target.currentAction.actionTimeLeft / target.currentAction.actionTimeFull;
		} else
		{
			actionSlider.gameObject.SetActive(false);
		}

		// Draw carried item
		itemIcon.gameObject.SetActive(target.carriesInventory);

		if (target.carriesInventory)
		{
			itemIconImage.sprite = target.carriedItem.icon;
			itemIconBackground.color = target.carriedItem.iconBackgroundColor;
		}
	}

	public void OnClick()
	{
		SelectionManager.SetSelection(target);
	}
}

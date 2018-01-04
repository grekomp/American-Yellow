using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIcon : MonoBehaviour {

	public Target target;

	public Transform targetIcon;
	public Image targetIconImage;
	public Image targetIconBackground;

	void Start () {
		targetIconImage = transform.Find("TargetIconImage").GetComponent<Image>();
		targetIconBackground = transform.Find("TargetIconBackground").GetComponent<Image>();

		targetIconImage.sprite = target.icon;
		targetIconBackground.color = target.iconBackgroundColor;
	}
	
	void Update () {
		if (target == null)
		{
			Destroy(gameObject);
		}
		
		

	}

	public void OnClick()
	{
		BeeUnit selection = SelectionManager.GetSelection();

		if (selection != null)
		{
			selection.ChangeTarget(target);
		}
	}
}

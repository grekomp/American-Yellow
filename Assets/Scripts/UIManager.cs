using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public UnitIcon unitIconPrefab;
	public GameObject targetIconPrefab;

	public Text scoreHoneyText;

	public static UIManager instance;

	void Awake () {
		instance = this;
	}
	
	void Update () {

	}

	public void AddUnitIcon(BeeUnit unit)
	{
		UnitIcon instantiatedIcon = Instantiate(unitIconPrefab, FolderHelper.instance.unitIcons) as UnitIcon;

		instantiatedIcon.unit = unit;
		instantiatedIcon.GetComponent<UIFollower>().target = unit.gameObject;
	}

	public void AddTargetIcon(Target target)
	{
		GameObject instantiatedIcon = Instantiate(targetIconPrefab, FolderHelper.instance.targetIcons) as GameObject;

		instantiatedIcon.GetComponent<TargetIcon>().target = target;
		instantiatedIcon.GetComponent<UIFollower>().target = target.gameObject;
	}

	public void UpdateScore(int scoreHoney)
	{
		scoreHoneyText.text = scoreHoney.ToString();
	}
}

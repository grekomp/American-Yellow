using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Transform unitIcons;
	public Transform targetIcons;

	public UnitIcon unitIconPrefab;
	public GameObject targetIconPrefab;

	static UIManager instance;

	void Awake () {
		instance = this;

		unitIcons = transform.Find("Canvas/UnitIcons");
		targetIcons = transform.Find("Canvas/TargetIcons");
	}
	
	void Update () {
		
	}

	public static void AddUnitIcon(BeeUnit unit)
	{
		instance._AddUnitIcon(unit);
	}

	void _AddUnitIcon(BeeUnit unit)
	{
		UnitIcon instantiatedIcon = Instantiate(unitIconPrefab, unitIcons) as UnitIcon;

		instantiatedIcon.target = unit;
		instantiatedIcon.GetComponent<UIFollower>().target = unit.gameObject;
	}

	public static void AddTargetIcon(Target target)
	{
		instance._AddTargetIcon(target);
	}

	void _AddTargetIcon(Target target)
	{
		GameObject instantiatedIcon = Instantiate(targetIconPrefab, targetIcons) as GameObject;

		instantiatedIcon.GetComponent<TargetIcon>().target = target;
		instantiatedIcon.GetComponent<UIFollower>().target = target.gameObject;
	}
}

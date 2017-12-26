using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
	public UnitModel Model;

	public Image UnitBacking;
	public Image UnitIcon;

	public TMP_Text UnitName;
	public TMP_Text UnitTypeName;

	public Image HPBar;

	// Use this for initialization
	void Start ()
	{
		UnitName.text = Model.UnitName;
		UnitTypeName.text = Model.UnitTypeName;
		UnitIcon.sprite = Model.Sprite;

		Model.HPChange += HandleHPPercentChange;

		HandleHPPercentChange(Model.HealthCurr / Model.HealthMax);
	}

	public void HandleHPPercentChange(float newHPPercent)
	{
		HPBar.fillAmount = newHPPercent;
	}
}

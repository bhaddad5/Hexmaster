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

	public TMP_Text FactionName;
	public TMP_Text UnitName;
	public TMP_Text UnitTypeName;

	public TMP_Text Attack;
	public TMP_Text Defense;

	public Image HPBar;

	// Use this for initialization
	void Start ()
	{
		FactionName.text = Model.Faction.FactionName;
		UnitName.text = Model.UnitName;
		UnitTypeName.text = Model.UnitTypeName;
		UnitIcon.sprite = Model.Sprite;
		Attack.text = Model.Attack.ToString();
		Defense.text = Model.Defense.ToString();
		UnitBacking.color = Model.Faction.FactionColor;

		HPBar.type = Image.Type.Filled;
		Model.HPChange += HandleHPPercentChange;

		HandleHPPercentChange(Model.HealthCurr / Model.HealthMax);

		Model.UpdateUnitPos += UpdateUnitPos;
		UpdateUnitPos(Model.CurrentPos);
	}

	public void HandleHPPercentChange(float newHPPercent)
	{
		HPBar.fillAmount = newHPPercent;
		HPBar.color = Color.Lerp(Color.red, Color.green, newHPPercent);

		if (newHPPercent <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void UpdateUnitPos(HexPos pos)
	{
		transform.position = MapInstantiator.GetHexPos(pos.X, pos.Z);
	}
}

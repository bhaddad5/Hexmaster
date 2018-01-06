using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexOccupier
{
	public HexPos CurrentPos;
}

[Serializable]
public class UnitModel : HexOccupier
{
	[NonSerialized]
	public FactionModel Faction;

	public float MovementCurr;
	public float MovementMax;

	public float Attack;
	public float Defense;

	public float Aggression;

	public float GetAttackValue()
	{
		return Attack;
	}

	public float GetDefenseValue()
	{
		return Defense + MapController.Model.GetHex(CurrentPos).DefenseMod;
	}

	public Sprite Sprite;

	public string UnitTypeName;
	public string UnitName;

	public float HealthMax;
	public float HealthCurr;
	public event Action<float> HPChange;
	public void InvokeUpdateHP(float newHP)
	{
		HealthCurr = newHP;
		if (HealthCurr <= 0)
		{
			MapController.RemoveUnit(this);
		}
		HPChange.Invoke(HealthCurr/HealthMax);
	}

	public event Action<HexPos> UpdateUnitPos;
	public void InvokeUpdateUnitPos(HexPos pos)
	{
		CurrentPos = pos;
		UpdateUnitPos.Invoke(pos);}
}

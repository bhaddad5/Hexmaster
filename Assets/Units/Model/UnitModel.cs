using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitModel
{
	[NonSerialized]
	public FactionModel Faction;

	public float MovementCurr;
	public float MovementMax;

	public float Attack;
	public float Defense;

	public float GetAttackValue()
	{
		return Attack;
	}

	public float GetDefenseValue()
	{
		return Defense + MapInstantiator.Model.GetHex(CurrentPos).DefenseMod;
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
			MapInstantiator.RemoveUnit(this);
		}
		HPChange.Invoke(HealthCurr/HealthMax);
	}

	public HexPos CurrentPos;
	public event Action<HexPos> UpdateUnitPos;
	public void InvokeUpdateUnitPos(HexPos pos)
	{
		CurrentPos = pos;
		UpdateUnitPos.Invoke(pos);}
}

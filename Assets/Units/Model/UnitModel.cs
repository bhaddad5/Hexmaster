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

	public Sprite Sprite;

	public string UnitTypeName;
	public string UnitName;

	public float HealthMax;
	public float HealthCurr;
	public event Action<float> HPChange;
	public void InvokeUpdateHP(float newHP)
	{
		HealthCurr = newHP;
		HPChange.Invoke(newHP);
	}

	public HexPos CurrentPos;
	public event Action<HexPos> UpdateUnitPos;
	public void InvokeUpdateUnitPos(HexPos pos)
	{
		CurrentPos = pos;
		UpdateUnitPos.Invoke(pos);}
}

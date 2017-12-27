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

	public float HealthMax;
	public float HealthCurr;

	public string UnitTypeName;
	public string UnitName;

	public event Action<float> HPChange;

	public Vector2 CurrentTile;
}

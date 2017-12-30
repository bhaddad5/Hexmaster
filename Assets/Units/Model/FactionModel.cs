using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public class FactionModel
{
	public Color FactionColor;
	public bool MainPlayerFaction = false;
	public string FactionName;

	public List<FactionModel> Allies = new List<FactionModel>();
	public List<FactionModel> Enemies = new List<FactionModel>();

	public bool PlayerControlAllowed()
	{
		foreach (FactionModel ally in Allies)
		{
			if (ally.MainPlayerFaction)
				return true;
		}
		return MainPlayerFaction;
	}

	public bool EnemyOfPlayer()
	{
		foreach (FactionModel enemy in Enemies)
		{
			if (enemy.PlayerControlAllowed())
				return true;
		}
		return false;
	}
}

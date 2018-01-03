using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public class HexPos
{
	public int X;
	public int Z;
	public string PlanetId;

	public HexPos(int x, int z)
	{
		X = x;
		Z = z;
	}

	public override string ToString()
	{
		return X + ", " + Z;
	}

	public override bool Equals(object obj)
	{
		return ((HexPos) obj).X == X && ((HexPos) obj).Z == Z;
	}
}

[Serializable]
public class HexModel
{
	[NonSerialized]
	public List<HexModel> Neighbors = new List<HexModel>();
	[NonSerialized]
	public HexPos Coord;

	public float MoveDifficulty;
	public float DefenseMod;
	public Sprite Sprite;

	public HexModel(float diff, float defense, Sprite sprite)
	{
		MoveDifficulty = diff;
		Sprite = sprite;
		DefenseMod = defense;
	}

	public enum HexHighlightTypes
	{
		None,
		Move,
		Attack
	}

	public event Action<HexHighlightTypes> TriggerHighlight;

	public void HighlightHex(HexHighlightTypes type)
	{
		TriggerHighlight.Invoke(type);
	}

	public List<HexModel> AttackableHexes(float movePoints, FactionModel faction)
	{
		List<HexModel> Attackable = new List<HexModel>();
		foreach (HexModel neighbor in Neighbors)
		{
			if (movePoints - neighbor.MoveDifficulty >= 0 && neighbor.ContainsEnemy(faction))
				Attackable.Add(neighbor);
		}
		return Attackable;
	}

	public Dictionary<HexModel, float> ReachableHexes(float movePoints, FactionModel faction)
	{
		Dictionary<HexModel, float> reachable = new Dictionary<HexModel, float>();

		SortedDupList<HexModel> moveFrontier = new SortedDupList<HexModel>();
		moveFrontier.Insert(this, movePoints);

		while (moveFrontier.Count > 0)
		{
			HexModel first = moveFrontier.TopValue();
			reachable[first] = moveFrontier.TopKey();
			if (!first.BordersEnemy(faction) || first.ContainsAlly(faction))
			{
				foreach (HexModel neighbor in first.Neighbors)
				{
					if (neighbor.MoveDifficulty >= 0 && !moveFrontier.ContainsValue(neighbor) && !reachable.ContainsKey(neighbor) && 
						moveFrontier.TopKey() - neighbor.MoveDifficulty >= 0 && !neighbor.ContainsNonAlliedUnit(faction))
						moveFrontier.Insert(neighbor, moveFrontier.TopKey() - neighbor.MoveDifficulty);
				}
			}
			moveFrontier.Pop();
		}

		List<HexModel> keysToRemove = new List<HexModel>();
		foreach (HexModel hex in reachable.Keys)
		{
			if(hex.ContainsUnit())
				keysToRemove.Add(hex);
		}
		foreach (HexModel hexToRemove in keysToRemove)
			reachable.Remove(hexToRemove);

		return reachable;
	}

	public bool ContainsUnit()
	{
		return MapInstantiator.Model.GetUnit(Coord) != null;
	}

	public bool ContainsAlly(FactionModel faction)
	{
		return ContainsUnit() && 
			(MapInstantiator.Model.GetUnit(Coord).Faction == faction || MapInstantiator.Model.GetUnit(Coord).Faction.Allies.Contains(faction));
	}

	public bool ContainsNonAlliedUnit(FactionModel faction)
	{
		return ContainsUnit() && 
			!(MapInstantiator.Model.GetUnit(Coord).Faction == faction || MapInstantiator.Model.GetUnit(Coord).Faction.Allies.Contains(faction));
	}

	public bool ContainsEnemy(FactionModel faction)
	{
		return ContainsUnit() && MapInstantiator.Model.GetUnit(Coord).Faction.Enemies.Contains(faction);
	}

	public bool BordersEnemy(FactionModel faction)
	{
		foreach (HexModel neighbor in Neighbors)
		{
			if (neighbor.ContainsEnemy(faction))
				return true;
		}
		return false;
	}
}

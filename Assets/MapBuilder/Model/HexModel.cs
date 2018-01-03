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

	public List<HexModel> AttackableHexes(float movePoints)
	{
		List<HexModel> Attackable = new List<HexModel>();
		foreach (HexModel neighbor in Neighbors)
		{
			if (movePoints - neighbor.MoveDifficulty >= 0 && neighbor.ContainsEnemy())
				Attackable.Add(neighbor);
		}
		return Attackable;
	}

	public Dictionary<HexModel, float> ReachableHexes(float movePoints)
	{
		Dictionary<HexModel, float> reachable = new Dictionary<HexModel, float>();

		SortedDupList<HexModel> moveFrontier = new SortedDupList<HexModel>();
		moveFrontier.Insert(this, movePoints);

		while (moveFrontier.Count > 0)
		{
			HexModel first = moveFrontier.TopValue();
			reachable[first] = moveFrontier.TopKey();
			if (!first.BordersEnemy() || first.ContainsAlly())
			{
				foreach (HexModel neighbor in first.Neighbors)
				{
					if (neighbor.MoveDifficulty >= 0 && !moveFrontier.ContainsValue(neighbor) && !reachable.ContainsKey(neighbor) && 
						moveFrontier.TopKey() - neighbor.MoveDifficulty >= 0 && !neighbor.ContainsNonAlliedUnit())
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

	public bool ContainsAlly()
	{
		return ContainsUnit() && MapInstantiator.Model.GetUnit(Coord).Faction.PlayerControlAllowed();
	}

	public bool ContainsNonAlliedUnit()
	{
		return ContainsUnit() && !MapInstantiator.Model.GetUnit(Coord).Faction.PlayerControlAllowed();
	}

	public bool ContainsEnemy()
	{
		return ContainsUnit() && MapInstantiator.Model.GetUnit(Coord).Faction.EnemyOfPlayer();
	}

	public bool BordersEnemy()
	{
		foreach (HexModel neighbor in Neighbors)
		{
			if (neighbor.ContainsEnemy())
				return true;
		}
		return false;
	}
}

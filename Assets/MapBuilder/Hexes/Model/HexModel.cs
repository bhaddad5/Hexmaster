using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexPos
{
	public int X;
	public int Z;

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

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}

[Serializable]
public class HexModel
{
	[NonSerialized]
	public List<HexModel> Neighbors = new List<HexModel>();
	[NonSerialized]
	public HexPos Coord;

	public List<HexOccupier> Occupants = new List<HexOccupier>();

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
		Attack,
		PotentialAttack
	}

	public event Action<HexHighlightTypes> TriggerHighlight;

	public void HighlightHex(HexHighlightTypes type)
	{
		TriggerHighlight.Invoke(type);
	}

	public MoveOptions PossibleMoves(float movePoints, FactionModel faction)
	{
		MoveOptions possibleMoves = new MoveOptions();

		SortedDupList<HexModel> moveFrontier = new SortedDupList<HexModel>();
		moveFrontier.Insert(this, movePoints);

		while (moveFrontier.Count > 0)
		{
			HexModel currHex = moveFrontier.TopValue();
			float currHexMoveRemaining = moveFrontier.TopKey();
			possibleMoves.Movable[currHex] = currHexMoveRemaining;
			if (!currHex.ContainsEnemy(faction))
			{
				foreach (HexModel neighbor in currHex.Neighbors)
				{
					if (neighbor.MoveDifficulty >= 0 && !moveFrontier.ContainsValue(neighbor) &&
					    !possibleMoves.Movable.ContainsKey(neighbor) &&
					    currHexMoveRemaining - neighbor.MoveDifficulty >= 0)
					{
						if (currHex.BordersEnemy(faction) && !neighbor.ContainsEnemy(faction) && !neighbor.ContainsAlly(faction) && neighbor.BordersEnemy(faction))
							continue;
						if(currHex.ContainsAlly(faction) && currHex != this && neighbor.ContainsEnemy(faction))
							continue;
						moveFrontier.Insert(neighbor, currHexMoveRemaining - neighbor.MoveDifficulty);
					}
				}
			}
			moveFrontier.Pop();
		}

		List<HexModel> hexesWithUnits = new List<HexModel>();
		foreach (HexModel hex in possibleMoves.Movable.Keys)
		{
			if(hex.ContainsUnit())
				hexesWithUnits.Add(hex);
		}
		foreach (HexModel hexToRemove in hexesWithUnits)
		{
			float reachableVal = possibleMoves.Movable[hexToRemove];
			possibleMoves.Movable.Remove(hexToRemove);
			if (hexToRemove.ContainsEnemy(faction))
			{
				if (Neighbors.Contains(hexToRemove))
					possibleMoves.Attackable[hexToRemove] = reachableVal;
				else possibleMoves.PotentialAttacks[hexToRemove] = reachableVal;
			}
		}

		return possibleMoves;
	}

	public bool ContainsUnit()
	{
		return MapController.Model.GetUnit(Coord) != null;
	}

	public bool ContainsAlly(FactionModel faction)
	{
		return ContainsUnit() && 
			(MapController.Model.GetUnit(Coord).Faction == faction || MapController.Model.GetUnit(Coord).Faction.Allies.Contains(faction));
	}

	public bool ContainsNonAlliedUnit(FactionModel faction)
	{
		return ContainsUnit() && 
			!(MapController.Model.GetUnit(Coord).Faction == faction || MapController.Model.GetUnit(Coord).Faction.Allies.Contains(faction));
	}

	public bool ContainsEnemy(FactionModel faction)
	{
		return ContainsUnit() && MapController.Model.GetUnit(Coord).Faction.Enemies.Contains(faction);
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

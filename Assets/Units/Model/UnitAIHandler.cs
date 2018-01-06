using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UnitAIHandler
{
	public class UnitMoves
	{
		public List<HexModel> Moves = new List<HexModel>();

		public UnitMoves(params HexModel[] moves)
		{
			Moves = moves.ToList();
		}
	}

	public static void ExecuteAIMove(UnitModel unit)
	{
		HexModel hex = MapController.Model.GetHex(unit.CurrentPos);

		MoveOptions possibleMoves = hex.PossibleMoves(unit.MovementCurr, unit.Faction);

		SortedDupList<UnitMoves> RankedMoves = new SortedDupList<UnitMoves>();
		RankedMoves.Insert(new UnitMoves(hex), hex.DefenseMod);

		foreach (HexModel potentialMove in possibleMoves.Movable.Keys)
		{
			RankedMoves.Insert(new UnitMoves(potentialMove), potentialMove.DefenseMod);
		}

		foreach (HexModel potentialAttack in possibleMoves.Attackable.Keys)
		{
			UnitModel UnitToAttack = MapController.Model.GetUnit(potentialAttack.Coord);
			RankedMoves.Insert(new UnitMoves(potentialAttack), hex.DefenseMod + GetAttackGoodness(unit, UnitToAttack));
		}

		foreach (KeyValuePair<HexModel, float> pair in possibleMoves.Movable)
		{
			foreach (HexModel attackableNeighbor in pair.Key.Neighbors)
			{
				if (pair.Value - attackableNeighbor.MoveDifficulty >= 0 &&
					attackableNeighbor.ContainsEnemy(unit.Faction))
				{
					UnitModel UnitToAttack = MapController.Model.GetUnit(attackableNeighbor.Coord);
					RankedMoves.Insert(new UnitMoves(pair.Key, attackableNeighbor), pair.Key.DefenseMod + GetAttackGoodness(unit, UnitToAttack));
				}
			}
		}

		Debug.Log(unit.UnitName + " ranked moves: ");
		foreach (KeyValuePair<float, UnitMoves> rankedMove in RankedMoves.GetList())
		{
			string moves = "";
			foreach (HexModel move in rankedMove.Value.Moves)
			{
				moves = moves + ", " + move.Coord.ToString();
			}
			Debug.Log(rankedMove.Key + ", " + moves);
		}

		ExecuteChosenMoves(unit, RankedMoves.TopValue());

	}

	private static float GetAttackGoodness(UnitModel unit, UnitModel unitToAttack)
	{
		float attackGoodness = (unit.GetAttackValue() / unitToAttack.GetDefenseValue()) - 1f;
		attackGoodness += unit.Aggression;
		return attackGoodness;
	}

	private static void ExecuteChosenMoves(UnitModel unit, UnitMoves chosenMoves)
	{
		foreach (HexModel move in chosenMoves.Moves)
		{
			MapController.MoveUnit(unit, move.Coord);
		}
	}
}

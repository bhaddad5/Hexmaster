using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOptions
{
	public Dictionary<HexModel, float> Movable = new Dictionary<HexModel, float>();
	public Dictionary<HexModel, float> Attackable = new Dictionary<HexModel, float>();
	public Dictionary<HexModel, float> PotentialAttacks = new Dictionary<HexModel, float>();
}

public class SelectedUnitController
{
	private UnitModel CurrSelectedUnit = null;
	private MoveOptions moveOptions = new MoveOptions();

	public void MoveAttempted(HexModel hex)
	{
		if (CurrSelectedUnit != null)
		{
			if (moveOptions.Movable.ContainsKey(hex))
			{
				MapController.MoveUnit(CurrSelectedUnit, hex.Coord);
				CurrSelectedUnit.MovementCurr = moveOptions.Movable[hex];
			}
			if (moveOptions.Attackable.ContainsKey(hex))
			{
				MapController.MoveUnit(CurrSelectedUnit, hex.Coord);
				if (CurrSelectedUnit.CurrentPos.Equals(hex.Coord))
					CurrSelectedUnit.MovementCurr -= hex.MoveDifficulty;
				else CurrSelectedUnit.MovementCurr = 0;
			}
			ClearSelectedUnit();
			HandleNewUnitSelected(CurrSelectedUnit);
		}
	}

	public void HandleNewUnitSelected(UnitModel unit)
	{
		CurrSelectedUnit = unit;

		moveOptions = MapController.Model.GetHex(unit.CurrentPos).PossibleMoves(unit.MovementCurr, unit.Faction);
		foreach (HexModel reachableHex in moveOptions.Movable.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.Move);
		foreach (HexModel reachableHex in moveOptions.Attackable.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.Attack);
		foreach (HexModel reachableHex in moveOptions.PotentialAttacks.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.PotentialAttack);
	}

	public void ClearSelectedUnit()
	{
		foreach (HexModel hex in MapController.Model.AllHexes())
			hex.HighlightHex(HexModel.HexHighlightTypes.None);
		moveOptions.Movable.Clear();
		moveOptions.Attackable.Clear();
		moveOptions.PotentialAttacks.Clear();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOptions
{
	public Dictionary<HexModel, float> Movable = new Dictionary<HexModel, float>();
	public Dictionary<HexModel, float> Attackable = new Dictionary<HexModel, float>();
	public Dictionary<HexModel, float> PotentialAttacks = new Dictionary<HexModel, float>();
}

public class MapInteractor : MonoBehaviour
{
	private UnitModel CurrSelectedUnit = null;
	private MoveOptions moveOptions = new MoveOptions();

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ClearSelected();
			HexModel hex = GetRaycastedHex();
			if (hex != null)
			{
				var unit = MapInstantiator.Model.Units[hex.Coord.X][hex.Coord.Z];
				if (unit != null)
					HandleNewUnitSelected(unit);
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			HexModel hex = GetRaycastedHex();
			if (hex != null)
			{
				if (CurrSelectedUnit != null)
				{
					if (moveOptions.Movable.ContainsKey(hex))
					{
						MapInstantiator.MoveUnit(CurrSelectedUnit, hex.Coord);
						CurrSelectedUnit.MovementCurr = moveOptions.Movable[hex];
					}
					if (moveOptions.Attackable.ContainsKey(hex))
					{
						MapInstantiator.AttackHex(CurrSelectedUnit, hex.Coord);
						if (CurrSelectedUnit.CurrentPos.Equals(hex.Coord))
							CurrSelectedUnit.MovementCurr -= hex.MoveDifficulty;
						else CurrSelectedUnit.MovementCurr = 0;
					}
					ClearSelected();
					HandleNewUnitSelected(CurrSelectedUnit);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			HexView[] hexViews = FindObjectsOfType(typeof(HexView)) as HexView[];
			foreach (HexView hexView in hexViews)
			{
				hexView.ToggleCoordinates();
			}
		}
	}

	private HexModel GetRaycastedHex()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			Transform objectHit = hit.transform;
			HexView result = objectHit.GetComponentInParent<HexView>();
			if (result != null)
				return result.HexModel;
		}
		return null;
	}

	private void HandleNewUnitSelected(UnitModel unit)
	{
		CurrSelectedUnit = unit;

		moveOptions = MapInstantiator.Model.Map[unit.CurrentPos.X][unit.CurrentPos.Z].PossibleMoves(unit.MovementCurr, unit.Faction);
		foreach (HexModel reachableHex in moveOptions.Movable.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.Move);
		foreach (HexModel reachableHex in moveOptions.Attackable.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.Attack);
		foreach (HexModel reachableHex in moveOptions.PotentialAttacks.Keys)
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.PotentialAttack);
	}

	private void ClearSelected()
	{
		foreach (HexModel hex in MapInstantiator.Model.AllHexes())
			hex.HighlightHex(HexModel.HexHighlightTypes.None);
		moveOptions.Movable.Clear();
		moveOptions.Attackable.Clear();
		moveOptions.PotentialAttacks.Clear();
	}

	public void HandleEndTurn()
	{
		ClearSelected();
		MapInstantiator.RefreshUnitMovements();
	}
}

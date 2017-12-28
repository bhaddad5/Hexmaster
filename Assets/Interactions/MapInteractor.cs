using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractor : MonoBehaviour
{
	private UnitModel CurrSelectedUnit = null;
	private Dictionary<HexModel, float> ReachableHexes = new Dictionary<HexModel, float>();

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
				if (CurrSelectedUnit != null && ReachableHexes.ContainsKey(hex))
				{
					MapInstantiator.MoveUnit(CurrSelectedUnit, hex.Coord);
					CurrSelectedUnit.MovementCurr = ReachableHexes[hex];
					ClearSelected();
					HandleNewUnitSelected(CurrSelectedUnit);
				}
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

		ReachableHexes = MapInstantiator.Model.Map[unit.CurrentPos.X][unit.CurrentPos.Z].ReachableHexes(unit.MovementCurr);
		foreach (HexModel reachableHex in ReachableHexes.Keys)
		{
			reachableHex.HighlightHex(HexModel.HexHighlightTypes.Move);
		}
	}

	private void ClearSelected()
	{
		foreach (HexModel hex in MapInstantiator.Model.AllHexes())
		{
			hex.HighlightHex(HexModel.HexHighlightTypes.None);
			ReachableHexes.Clear();
		}
	}

	public void HandleEndTurn()
	{
		ClearSelected();
		MapInstantiator.RefreshUnitMovements();
	}
}

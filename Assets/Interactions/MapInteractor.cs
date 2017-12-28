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
					HandleNewUnitSelected(hex.Coord, unit);
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			HexModel hex = GetRaycastedHex();
			if (hex != null)
			{
				if (CurrSelectedUnit != null && ReachableHexes.ContainsKey(hex))
				{
					Debug.Log("Moving " + CurrSelectedUnit.UnitName + " to " + hex.Coord);
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

	private void HandleNewUnitSelected(HexPos pos, UnitModel unit)
	{
		CurrSelectedUnit = unit;

		Debug.Log("Selected " + CurrSelectedUnit.UnitName);

		ReachableHexes = MapInstantiator.Model.Map[pos.X][pos.Z].ReachableHexes(unit.MovementCurr);
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
}

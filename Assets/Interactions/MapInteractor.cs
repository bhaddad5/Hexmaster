using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractor : MonoBehaviour
{
	private UnitModel CurrSelectedUnit = null;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Transform objectHit = hit.transform;
				HexView result = objectHit.GetComponentInParent<HexView>();
				if (result != null)
				{
					Debug.Log("me: " + result.HexModel.Coord);
					var unit = MapInstantiator.Model.Units[result.HexModel.Coord.X][result.HexModel.Coord.Z];
					ClearSelected();
					if (unit != null)
						HandleNewUnitSelected(result.HexModel.Coord, unit);
				}
			}
		}
	}

	private void HandleNewUnitSelected(HexPos pos, UnitModel unit)
	{
		CurrSelectedUnit = unit;

		Debug.Log("Selected " + CurrSelectedUnit.UnitName);

		MapInstantiator.Model.Map[pos.X][pos.Z].HighlightHex(HexModel.HexHighlightTypes.Move);
	}

	private void ClearSelected()
	{
		foreach (HexModel hex in MapInstantiator.Model.AllHexes())
		{
			hex.HighlightHex(HexModel.HexHighlightTypes.None);
		}
	}
}

using UnityEngine;

public class MapInteractor : MonoBehaviour
{
	private SelectedUnitController SelectedUnitController = new SelectedUnitController();
	private SelectedPlanetController SelectedPlanetController = new SelectedPlanetController();

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedUnitController.ClearSelectedUnit();
			HexModel hex = GetRaycastedHex();
			if (hex != null)
			{
				HandleHexSelection(hex);
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			HexModel hex = GetRaycastedHex();
			if (hex != null)
			{
				
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

	private void HandleHexSelection(HexModel hex)
	{
		foreach (HexOccupier occupant in hex.Occupants)
		{
			if (occupant is UnitModel)
			{
				if(((UnitModel)occupant).Faction.PlayerControlAllowed())
					SelectedUnitController.HandleNewUnitSelected((UnitModel)occupant);
			}
			if (occupant is PlanetModel)
			{
				SelectedPlanetController.HandleNewSelectedPlanet((PlanetModel)occupant);
			}
		}
	}

	public void HandleEndTurn()
	{
		SelectedUnitController.ClearSelectedUnit();
		MapController.ExecuteUnitAI();
		MapController.RefreshUnitMovements();
	}

	public void ViewSelectedPlanet()
	{
		SelectedPlanetController.ViewSelectedPlanet();
	}
}

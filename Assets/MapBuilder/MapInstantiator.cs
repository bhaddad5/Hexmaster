using System.Collections.Generic;
using UnityEngine;

public class MapInstantiator : MonoBehaviour
{
	public HexView HexPrefab;
	public UnitView UnitPrefab;
	public PlanetView PlanetPrefab;

	public void InstantiateMap(HexModel[][] model)
	{
		MapController.Model = new GalaxyModel(model);
		InstantiateHexes();
	}

	public void InstantiateHexes()
	{
		foreach (HexModel hex in MapController.Model.AllHexes())
		{
			HexView newHex = GameObject.Instantiate(HexPrefab);
			newHex.HexModel = hex;
			newHex.transform.position = GetHexWorldPos(hex.Coord);

			InstantiateHexOccupants(hex.Coord, hex.Occupants);
		}
	}

	private void InstantiateHexOccupants(HexPos pos, List<HexOccupier> occupants)
	{
		foreach (HexOccupier occupant in occupants)
		{
			if (occupant is UnitModel)
			{
				UnitView newUnit = GameObject.Instantiate(UnitPrefab);
				occupant.CurrentPos = pos;
				newUnit.Model = (UnitModel)occupant;
			}
			if (occupant is PlanetModel)
			{
				PlanetView newPlanet = GameObject.Instantiate(PlanetPrefab);
				occupant.CurrentPos = pos;
				newPlanet.Model = (PlanetModel) occupant;
			}
		}
	}

	private const float outerRadius = 0.5f;
	private const float innerRadius = outerRadius * 0.866025404f;
	public static Vector3 GetHexWorldPos(HexPos pos)
	{
		Vector3 position = new Vector3();
		position.x = (pos.X + pos.Z * 0.5f - pos.Z / 2) * (innerRadius * 2f);
		position.y = 0f;
		position.z = pos.Z * (outerRadius * 1.5f);
		return position;
	}
}
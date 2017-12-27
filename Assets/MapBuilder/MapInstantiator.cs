using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class HexMetrics
{
	public const float outerRadius = 0.5f;

	public const float innerRadius = outerRadius * 0.866025404f;
}

public class MapInstantiator : MonoBehaviour
{
	public static MapModel Model;

	public HexView HexPref;
	public UnitView UnitPref;
	public static HexView HexPrefab;
	public static UnitView UnitPrefab;

	void Awake()
	{
		HexPrefab = HexPref;
		UnitPrefab = UnitPref;
	}

	public static void InstantiateMap(MapModel model)
	{
		Model = model;
		InstantiateHexes(model.Map);
		InstantiateUnits(model.Units);
	}

	public static void InstantiateHexes(HexModel[][] Hexes)
	{
		int x = 0;
		foreach (HexModel[] hexModels in Hexes)
		{
			int z = 0;
			foreach (HexModel hex in hexModels)
			{
				HexView newHex = GameObject.Instantiate(HexPrefab);
				newHex.HexModel = hex;
				newHex.transform.position = GetHexPos(x, z);

				z++;
			}
			x++;
		}
	}

	public static void InstantiateUnits(UnitModel[][] Units)
	{
		int x = 0;
		foreach (UnitModel[] unitModels in Units)
		{
			int z = 0;
			foreach (UnitModel unit in unitModels)
			{
				if (unit != null)
				{
					UnitView newUnit = GameObject.Instantiate(UnitPrefab);
					newUnit.Model = unit;
					newUnit.transform.position = GetHexPos(x, z);
					newUnit.transform.position += new Vector3(0, 0.01f, 0);
				}
				z++;
			}
			x++;
		}
	}

	private static Vector3 GetHexPos(int x, int z)
	{
		Vector3 position = new Vector3();
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);
		return position;
	}
}
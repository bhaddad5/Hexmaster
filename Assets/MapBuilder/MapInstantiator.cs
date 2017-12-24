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
	public HexView HexPref;
	public static HexView HexPrefab;
	void Awake(){HexPrefab = HexPref;}

	public static void InstantiateMap(MapModel model)
	{
		int x = 0;
		foreach (HexModel[] hexModels in model.Map)
		{
			int z = 0;
			foreach (HexModel hex in hexModels)
			{
				HexView newHex = GameObject.Instantiate(HexPrefab);
				newHex.HexModel = hex;

				Vector3 position = new Vector3();
				position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
				position.y = 0f;
				position.z = z * (HexMetrics.outerRadius * 1.5f);
				newHex.transform.position = position;

				z++;
			}
			x++;
		}
	}
}
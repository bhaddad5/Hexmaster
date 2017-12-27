using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MapModel
{
	public HexModel[][] Map;

	public UnitModel[][] Units;

	public void SetUpAdjacencies()
	{
		int x = 0;
		foreach (HexModel[] column in Map)
		{
			int z = 0;
			foreach (HexModel hex in column)
			{
				SetUpHexAdjacencies(hex, x, z);
				hex.Coord = new HexPos(x, z);
				z++;
			}
			x++;
		}
	}

	public List<HexModel> AllHexes()
	{
		List<HexModel> hexes = new List<HexModel>();
		foreach (HexModel[] hexModels in Map)
		{
			hexes = hexes.Concat(hexModels).ToList();
		}
		return hexes;
	} 

	private void SetUpHexAdjacencies(HexModel hex, int x, int z)
	{
		if (z % 2 == 1)
		{
			TryAddAdjacency(x, z + 1, hex);
			TryAddAdjacency(x - 1, z, hex);
			TryAddAdjacency(x, z - 1, hex);
			TryAddAdjacency(x + 1, z - 1, hex);
			TryAddAdjacency(x + 1, z, hex);
			TryAddAdjacency(x + 1, z + 1, hex);
		}
		else
		{
			TryAddAdjacency(x - 1, z + 1, hex);
			TryAddAdjacency(x - 1, z, hex);
			TryAddAdjacency(x - 1, z - 1, hex);
			TryAddAdjacency(x, z - 1, hex);
			TryAddAdjacency(x + 1, z, hex);
			TryAddAdjacency(x, z + 1, hex);
		}
	}

	private void TryAddAdjacency(int x, int z, HexModel hex)
	{
		if(x < Map.Length && x >= 0 &&
			z < Map[0].Length && z >= 0)
			hex.Neighbors.Add(Map[x][z]);
	}
}

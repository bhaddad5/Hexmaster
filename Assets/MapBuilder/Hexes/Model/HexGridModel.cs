using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class HexGridModel
{
	private HexModel[][] Map;

	public HexGridModel(HexModel[][] map)
	{
		Map = map;
		SetUpAdjacencies();
	}

	private void SetUpAdjacencies()
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

	public HexModel GetHex(HexPos pos)
	{
		return Map[pos.X][pos.Z];
	}

	public UnitModel GetUnit(HexPos pos)
	{
		foreach (HexOccupier occupant in GetHex(pos).Occupants)
		{
			if (occupant is UnitModel)
				return (UnitModel)occupant;
		}
		return null;
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

	public void ClearAllHighlights()
	{
		foreach (HexModel hex in AllHexes())
			hex.HighlightHex(HexModel.HexHighlightTypes.None);
	}
}

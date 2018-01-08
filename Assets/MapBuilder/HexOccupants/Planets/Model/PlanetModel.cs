using System;
using System.Collections.Generic;

[Serializable]
public class PlanetModel : HexOccupier
{
	public string PlanetName;

	private HexGridModel Map;

	public PlanetModel(HexModel[][] map)
	{
		Map = new HexGridModel(map);
	}

	public HexModel GetHex(HexPos pos) { return Map.GetHex(pos); }
	public UnitModel GetUnit(HexPos pos) { return Map.GetUnit(pos); }
	public List<HexModel> AllHexes() { return Map.AllHexes(); }
}
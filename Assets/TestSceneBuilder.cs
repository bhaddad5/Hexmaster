using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public Sprite Grass;
	public Sprite City;
	public Sprite Forest;

	public Sprite ImpGuard;
	public Sprite ChaosGuard;

	private HexModel GrassHex { get { return new HexModel(.5f, 0f, Grass); } }
	private HexModel ForestHex { get { return new HexModel(.9f, 1f, Forest); } }
	private HexModel CityHex { get { return new HexModel(.5f, 3f, City); } }

	// Use this for initialization
	void Start ()
	{
		MapModel Map = new MapModel();
		Map.Map = new HexModel[4][];
		Map.Map[0] = new[] { ForestHex, ForestHex, GrassHex, GrassHex };
		Map.Map[1] = new[] { ForestHex, ForestHex, GrassHex, GrassHex };
		Map.Map[2] = new[] { ForestHex, GrassHex, CityHex, GrassHex };
		Map.Map[3] = new[] { GrassHex, GrassHex, CityHex, GrassHex };
		Map.SetUpAdjacencies();

		Map.Units = new UnitModel[Map.Map.Length][];
		for (int i = 0; i < Map.Units.Length; i++)
			Map.Units[i] = new UnitModel[Map.Map[0].Length];

		Map.Units[1][2] = new UnitModel()
		{
			Attack = 1f,
			Defense = 3f,
			HealthCurr = 1f,
			HealthMax = 1f,
			MovementCurr = 1f,
			MovementMax = 1f,
			Sprite = ImpGuard,
			UnitName = "Thracian 503<sup>rd</sup>",
			UnitTypeName = "Imperial Guard"
		};

		Map.Units[3][3] = new UnitModel()
		{
			Attack = 1.2f,
			Defense = 2f,
			HealthCurr = 0.5f,
			HealthMax = 1f,
			MovementCurr = 1f,
			MovementMax = 1f,
			Sprite = ChaosGuard,
			UnitName = "Gorlaks Reavers",
			UnitTypeName = "Traitor Guard"
		};

		MapInstantiator.InstantiateMap(Map);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public Sprite Grass;
	public Sprite City;

	public Sprite ImpGuard;
	public Sprite ChaosGuard;

	// Use this for initialization
	void Start () {
		MapModel Map = new MapModel();
		Map.Map = new HexModel[4][];
		Map.Map[0] = new[] {new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass) };
		Map.Map[1] = new[] { new HexModel(2, City), new HexModel(2, City), new HexModel(1, Grass), new HexModel(1, Grass) };
		Map.Map[2] = new[] { new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass) };
		Map.Map[3] = new[] { new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass) };
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
			Movement = 1f,
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
			Movement = 1f,
			Sprite = ChaosGuard,
			UnitName = "Gorlaks Reavers",
			UnitTypeName = "Traitor Guard"
		};

		MapInstantiator.InstantiateMap(Map);
	}
}

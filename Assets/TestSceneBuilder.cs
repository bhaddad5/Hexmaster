using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public Sprite Grass;
	public Sprite City;
	public Sprite Forest;

	public Sprite Infantry;

	private HexModel GrassHex { get { return new HexModel(.5f, 0f, Grass); } }
	private HexModel ForestHex { get { return new HexModel(.9f, 1f, Forest); } }
	private HexModel CityHex { get { return new HexModel(.5f, 3f, City); } }

	private UnitModel ImperialGuard { get { return new UnitModel()
	{
		Attack = 1f,
		Defense = 3f,
		HealthCurr = 1f,
		HealthMax = 1f,
		MovementCurr = 1f,
		MovementMax = 1f,
		Sprite = Infantry,
		UnitTypeName = "Imperial Guard"
	}; } }

	private UnitModel TraitorGuard
	{
		get { return new UnitModel()
			{
				Attack = 1f,
				Defense = 2f,
				HealthCurr = 1f,
				HealthMax = 1f,
				MovementCurr = 1f,
				MovementMax = 1f,
				Sprite = Infantry,
				UnitTypeName = "Traitor Guard"
		};
		}
	}

	// Use this for initialization
	void Start ()
	{
		FactionModel Imperium = new FactionModel()
		{
			FactionColor = new Color(.1f, .5f, .1f),
			FactionName = "Imperium of Man",
			PlayerControlled = true
		};

		FactionModel ThracianTraitors = new FactionModel()
		{
			FactionColor = new Color(.9f, .1f, .1f),
			FactionName = "Thracian Traitors",
			PlayerControlled = true
		};

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

		var T503 = ImperialGuard;
		T503.UnitName = "Thracian 503<sup>rd</sup>";
		T503.Faction = Imperium;
		Map.Units[1][2] = T503;

		var Gorlak = TraitorGuard;
		Gorlak.UnitName = "Gorlaks Reavers";
		Gorlak.HealthCurr = 0.5f;
		Gorlak.Faction = ThracianTraitors;
		Map.Units[2][2] = Gorlak;

		MapInstantiator.InstantiateMap(Map);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public Sprite Grass;
	public Sprite City;
	public Sprite Forest;

	public Sprite Infantry;
	public Sprite Cavalry;

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

	private UnitModel ImperialRoughRider{get{return new UnitModel()
	{
		Attack = 2f,
		Defense = 1f,
		HealthCurr = 1f,
		HealthMax = 1f,
		MovementCurr = 2f,
		MovementMax = 2f,
		Sprite = Cavalry,
		UnitTypeName = "Rough Riders"
	};}}

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
				UnitTypeName = "Traitor Infantry"
		};
		}
	}
	private UnitModel TraitorRoughRider
	{
		get
		{
			return new UnitModel()
			{
				Attack = 2f,
				Defense = 1f,
				HealthCurr = 1f,
				HealthMax = 1f,
				MovementCurr = 2f,
				MovementMax = 2f,
				Sprite = Cavalry,
				UnitTypeName = "Chaos Raiders"
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
			MainPlayerFaction = true,
		};

		FactionModel KimernaPdf = new FactionModel()
		{
			FactionColor = new Color(.5f, .5f, .8f),
			FactionName = "Kimerna Defense Force",
		};

		FactionModel ChaosRaiders = new FactionModel()
		{
			FactionColor = new Color(.9f, .1f, .1f),
			FactionName = "Chaos Raiders",
		};

		Imperium.Allies = new List<FactionModel>() {KimernaPdf};
		Imperium.Enemies = new List<FactionModel>() {ChaosRaiders};

		KimernaPdf.Allies = new List<FactionModel>() { Imperium };
		KimernaPdf.Enemies = new List<FactionModel>() { ChaosRaiders };

		ChaosRaiders.Allies = new List<FactionModel>() {  };
		ChaosRaiders.Enemies = new List<FactionModel>() { KimernaPdf, Imperium };

		MapModel Map = new MapModel();
		Map.Map = new HexModel[][]
		{
			new[] { ForestHex, ForestHex, GrassHex, GrassHex, GrassHex },
			new[] { ForestHex, ForestHex, GrassHex, GrassHex, GrassHex },
			new[] { ForestHex, GrassHex, CityHex, GrassHex, GrassHex },
			new[] { GrassHex, GrassHex, CityHex, GrassHex, GrassHex },
			new[] { GrassHex, GrassHex, GrassHex, GrassHex, GrassHex },
			new[] { GrassHex, ForestHex, GrassHex, GrassHex, GrassHex },
			new[] { GrassHex, ForestHex, ForestHex, ForestHex, GrassHex }
		};
		Map.SetUpAdjacencies();

		Map.Units = new UnitModel[Map.Map.Length][];
		for (int i = 0; i < Map.Units.Length; i++)
			Map.Units[i] = new UnitModel[Map.Map[0].Length];

		var T503 = ImperialGuard;
		T503.UnitName = "Thracian 503<sup>rd</sup>";
		T503.Faction = Imperium;
		Map.Units[1][2] = T503;

		var KimernaKnights = ImperialRoughRider;
		KimernaKnights.UnitName = "4<sup>th</sup> Knights of Kimerthas";
		KimernaKnights.Faction = KimernaPdf;
		Map.Units[3][4] = KimernaKnights;

		var Gorlak = TraitorRoughRider;
		Gorlak.UnitName = "Gorlaks Reavers";
		Gorlak.HealthCurr = 0.9f;
		Gorlak.Faction = ChaosRaiders;
		Map.Units[2][3] = Gorlak;

		var Fargren = TraitorGuard;
		Fargren.UnitName = "Fargren's Rifles";
		Fargren.HealthCurr = 0.1f;
		Fargren.Faction = ChaosRaiders;
		Map.Units[2][2] = Fargren;

		MapInstantiator.InstantiateMap(Map);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public MapInstantiator Instantiator;

	public Sprite Grass;
	public Sprite City;
	public Sprite Forest;
	public Sprite Ocean;
	public Sprite Mountain;

	public Sprite Space;
	public Sprite WarpStorm;

	private HexModel GrassHex { get { return new HexModel(.5f, 0f, Grass); } }
	private HexModel ForestHex { get { return new HexModel(.75f, 1f, Forest); } }
	private HexModel CityHex { get { return new HexModel(.5f, 2f, City); } }
	private HexModel OceanHex { get { return new HexModel(-1f, 0f, Ocean); } }
	private HexModel MountainHex { get { return new HexModel(-1f, 4f, Mountain); } }

	public HexModel WarpStormHex { get { return new HexModel(-1f, 0f, WarpStorm);} }
	public HexModel SpaceHex { get { return new HexModel(0.2f, 0f, Space); } }

	public Sprite Infantry;
	public Sprite Cavalry;
	public Sprite Artillery;
	public Sprite Armor;

	private UnitModel ImperialGuard { get { return new UnitModel()
	{
		Attack = 5f,
		Defense = 7f,
		Aggression = 0f,
		HealthCurr = 5f,
		HealthMax = 5f,
		MovementCurr = 1f,
		MovementMax = 1f,
		Sprite = Infantry,
		UnitTypeName = "Guard Regiment"
	}; } }

	private UnitModel ImperialPDF { get { return new UnitModel()
	{
		Attack = 3f,
		Defense = 4f,
		Aggression = 0f,
		HealthCurr = 4f,
		HealthMax = 4f,
		MovementCurr = 1f,
		MovementMax = 1f,
		Sprite = Infantry,
		UnitTypeName = "PDF Regiment"
	}; } }

	private UnitModel ImperialArtillery { get { return new UnitModel()
	{
		Attack = 8f,
		Defense = 3f,
		Aggression = 2f,
		HealthCurr = 3f,
		HealthMax = 3f,
		MovementCurr = 1f,
		MovementMax = 1f,
		Sprite = Artillery,
		UnitTypeName = "Artillery Regiment"
	}; } }

	private UnitModel ImperialArmor { get { return new UnitModel()
	{
		Attack = 9f,
		Defense = 7f,
		Aggression = 3f,
		HealthCurr = 6f,
		HealthMax = 6f,
		MovementCurr = 2f,
		MovementMax = 2f,
		Sprite = Armor,
		UnitTypeName = "Armored Regiment"
	}; } }

	private UnitModel ImperialRoughRider{get{return new UnitModel()
	{
		Attack = 7f,
		Defense = 4f,
		Aggression = 2f,
		HealthCurr = 4f,
		HealthMax = 4f,
		MovementCurr = 2f,
		MovementMax = 2f,
		Sprite = Cavalry,
		UnitTypeName = "Rough Riders"
	};}}

	private UnitModel TraitorGuard{get { return new UnitModel()
	{
		Attack = 6f,
		Defense = 6f,
		Aggression = 2f,
		HealthCurr = 5f,
		HealthMax = 5f,
		MovementCurr = 1f,
		MovementMax = 1f,
		Sprite = Infantry,
		UnitTypeName = "Traitor Infantry"
	};}}

	private UnitModel TraitorRoughRider{get{return new UnitModel()
	{
		Attack = 9f,
		Defense = 3f,
		Aggression = 2f,
		HealthCurr = 4f,
		HealthMax = 4f,
		MovementCurr = 2f,
		MovementMax = 2f,
		Sprite = Cavalry,
		UnitTypeName = "Chaos Riders"
	};}}

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
			FactionName = "Kimerna PDF",
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

		HexModel[][] Galaxy = new HexModel[][]
		{
			new []{SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex},
			new []{SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex},
			new []{SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex},
			new []{SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex},
			new []{SpaceHex, SpaceHex, WarpStormHex, WarpStormHex, WarpStormHex, WarpStormHex, SpaceHex, SpaceHex, WarpStormHex, SpaceHex},
			new []{SpaceHex, WarpStormHex, WarpStormHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, WarpStormHex, SpaceHex},
			new []{SpaceHex, WarpStormHex, WarpStormHex, SpaceHex, SpaceHex, SpaceHex, SpaceHex, WarpStormHex, WarpStormHex, SpaceHex},
		};

		HexModel[][] Kimerna = new HexModel[][]
		{
			new[] { ForestHex, ForestHex, GrassHex, GrassHex, GrassHex, OceanHex, OceanHex },
			new[] { ForestHex, ForestHex, GrassHex, GrassHex, GrassHex, OceanHex, OceanHex },
			new[] { ForestHex, GrassHex, CityHex, GrassHex, ForestHex, ForestHex, OceanHex },
			new[] { GrassHex, GrassHex, CityHex, GrassHex, ForestHex, ForestHex, OceanHex },
			new[] { GrassHex, GrassHex, GrassHex, GrassHex, GrassHex, ForestHex, OceanHex },
			new[] { GrassHex, ForestHex, GrassHex, GrassHex, CityHex, OceanHex, OceanHex },
			new[] { GrassHex, ForestHex, ForestHex, ForestHex, GrassHex, OceanHex, OceanHex },
			new[] { GrassHex, ForestHex, MountainHex, MountainHex, GrassHex, OceanHex, OceanHex },
			new[] { GrassHex, MountainHex, MountainHex, ForestHex, MountainHex, OceanHex, OceanHex }
		};

		var T503 = ImperialGuard;
		T503.UnitName = "Thracian 503<sup>rd</sup>";
		T503.Faction = Imperium;
		Kimerna[0][4].Occupants.Add(T503);

		var T513 = ImperialArmor;
		T513.UnitName = "Thracian 513<sup>th</sup>";
		T513.Faction = Imperium;
		Kimerna[0][3].Occupants.Add(T513);

		var T523 = ImperialArtillery;
		T523.UnitName = "Thracian 523<sup>rd</sup>";
		T523.Faction = Imperium;
		Kimerna[0][2].Occupants.Add(T523);

		var T1224 = ImperialGuard;
		T1224.UnitName = "Thracian 1224<sup>th</sup>";
		T1224.Faction = Imperium;
		Kimerna[0][1].Occupants.Add(T1224);


		var KimernaKnights = ImperialRoughRider;
		KimernaKnights.UnitName = "4<sup>th</sup> Kimernas Lancers";
		KimernaKnights.Faction = KimernaPdf;
		Kimerna[5][0].Occupants.Add(KimernaKnights);

		var K_PDF_35 = ImperialPDF;
		K_PDF_35.UnitName = "35<sup>th</sup> Kimernas PDF";
		K_PDF_35.Faction = KimernaPdf;
		Kimerna[5][1].Occupants.Add(K_PDF_35);

		var K_PDF_15 = ImperialPDF;
		K_PDF_15.UnitName = "15<sup>th</sup> Kimernas PDF";
		K_PDF_15.Faction = KimernaPdf;
		Kimerna[6][0].Occupants.Add(K_PDF_15);



		var Gorlak = TraitorRoughRider;
		Gorlak.UnitName = "Gorlak's Reavers";
		Gorlak.HealthCurr = 2.9f;
		Gorlak.Faction = ChaosRaiders;
		Kimerna[2][3].Occupants.Add(Gorlak);

		var DoK8 = TraitorGuard;
		DoK8.UnitName = "8<sup>th</sup> Desciples of Karnor";
		DoK8.Faction = ChaosRaiders;
		Kimerna[2][2].Occupants.Add(DoK8);

		var K_PDF_67 = ImperialPDF;
		K_PDF_67.UnitName = "67<sup>th</sup> Kimernas PDF";
		K_PDF_67.Faction = ChaosRaiders;
		Kimerna[3][2].Occupants.Add(K_PDF_67);

		var K_PDF_4 = ImperialPDF;
		K_PDF_4.UnitName = "4<sup>th</sup> Kimernas PDF";
		K_PDF_4.Faction = ChaosRaiders;
		Kimerna[3][1].Occupants.Add(K_PDF_4);

		var K_PDF_16 = ImperialPDF;
		K_PDF_16.UnitName = "16<sup>th</sup> Kimernas PDF";
		K_PDF_16.Faction = ChaosRaiders;
		Kimerna[5][4].Occupants.Add(K_PDF_16);

		Instantiator.InstantiateMap(Galaxy);
	}
}

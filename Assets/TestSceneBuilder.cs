using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneBuilder : MonoBehaviour
{
	public Sprite Grass;
	public Sprite City;

	// Use this for initialization
	void Start () {
		MapModel Map = new MapModel();
		Map.Map = new HexModel[3][];
		Map.Map[0] = new[] {new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass) };
		Map.Map[1] = new[] { new HexModel(2, City), new HexModel(2, City), new HexModel(1, Grass) };
		Map.Map[2] = new[] { new HexModel(1, Grass), new HexModel(1, Grass), new HexModel(1, Grass) };

		MapInstantiator.InstantiateMap(Map);
	}
}

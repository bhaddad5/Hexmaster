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
		Map.Map[0] = new[] {new HexModel(1, Grass, new Vector2(0, 0)), new HexModel(1, Grass, new Vector2(0, 1)), new HexModel(1, Grass, new Vector2(0, 2)) };
		Map.Map[1] = new[] { new HexModel(2, City, new Vector2(1, 0)), new HexModel(2, City, new Vector2(1, 1)), new HexModel(1, Grass, new Vector2(1, 2)) };
		Map.Map[2] = new[] { new HexModel(1, Grass, new Vector2(2, 0)), new HexModel(1, Grass, new Vector2(2, 1)), new HexModel(1, Grass, new Vector2(2, 2)) };
		Map.SetUpAdjacencies();

		MapInstantiator.InstantiateMap(Map);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexModel : MonoBehaviour
{
	public List<HexModel> Neighbors = new List<HexModel>();
	public float Difficulty;
	public Sprite Sprite;
	public Vector2 Coord;

	public HexModel(float diff, Sprite sprite, Vector2 coord)
	{
		Difficulty = diff;
		Sprite = sprite;
		Coord = coord;
	}
}

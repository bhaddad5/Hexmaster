using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class HexModel
{
	[NonSerialized]
	public List<HexModel> Neighbors = new List<HexModel>();

	public float Difficulty;
	public Sprite Sprite;
	public Vector2 Coord;

	public HexModel(float diff, Sprite sprite)
	{
		Difficulty = diff;
		Sprite = sprite;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class HexPos
{
	public int X;
	public int Z;
	public string PlanetId;

	public HexPos(int x, int z)
	{
		X = x;
		Z = z;
	}

	public override string ToString()
	{
		return X + ", " + Z;
	}
}

[Serializable]
public class HexModel
{
	[NonSerialized]
	public List<HexModel> Neighbors = new List<HexModel>();
	[NonSerialized]
	public HexPos Coord;

	public float Difficulty;
	public Sprite Sprite;

	public HexModel(float diff, Sprite sprite)
	{
		Difficulty = diff;
		Sprite = sprite;
	}
}

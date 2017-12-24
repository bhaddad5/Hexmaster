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

	public HexModel(float diff, Sprite sprite)
	{
		Difficulty = diff;
		Sprite = sprite;
	}
}

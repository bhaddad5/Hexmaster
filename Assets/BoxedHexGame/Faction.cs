using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
	public Color Color;

	public Unit[] Units;
	public bool UseAi;

	void EndTurn()
	{
		foreach (Unit unit in Units)
		{
			
		}
	}
}

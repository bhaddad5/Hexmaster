using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public Map Map;
	public List<Faction> Factions;

	void Start()
	{
		Map.Visuals.DisplayMap();
	}

	public void ExecuteEndTurn()
	{
		foreach (Faction faction in Factions)
		{
			faction.EndTurn();
		}
	}
}

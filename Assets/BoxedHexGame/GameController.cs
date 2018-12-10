using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public List<Faction> Factions;

	public void ExecuteEndTurn()
	{
		foreach (Faction faction in Factions)
		{
			faction.EndTurn();
		}
	}
}

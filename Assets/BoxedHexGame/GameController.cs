using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public List<Faction> Factions;

	public float ExecuteEndTurn()
	{
		foreach (Faction faction in Factions)
		{
			foreach (Unit unit in faction.Units)
			{
				unit.MoveTo(unit.GetAiMove());
				unit.Attack(unit.GetAiAttckNode());
			}
		}
	}
}

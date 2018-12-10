using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
	public FactionVisuals Visuals;

	public List<Faction> Allies;

	public List<Unit> Units = new List<Unit>();
	public bool UseAi;

	public void EndTurn()
	{
		foreach (Unit unit in Units)
		{
			if (UseAi)
			{
				unit.MoveTo(MoveHelpers.GetAiMove(unit));
				unit.Attack(MoveHelpers.GetAiAttckNode(unit.CurrNode, unit.Faction, unit));
			}
			unit.Refresh();
		}
	}
}

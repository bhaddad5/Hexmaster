using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
	public Color Color;

	public List<Faction> Allies;

	public List<Unit> Units = new List<Unit>();
	public bool UseAi;

	public void EndTurn()
	{
		foreach (Unit unit in Units)
		{
			if (UseAi)
			{
				unit.MoveTo(unit.GetAiMove());
				unit.Attack(unit.GetAiAttckNode());
			}
			unit.Refresh();
		}
	}
}

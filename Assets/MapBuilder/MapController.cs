public static class MapController
{
	public static GalaxyModel Model;

	public static void MoveUnit(UnitModel unit, HexPos newPos)
	{
		if (Model.GetUnit(newPos) != null && Model.GetUnit(newPos) != unit)
		{
			HandleCombat(unit, Model.GetUnit(newPos), Model.GetHex(newPos));
			if (Model.GetUnit(newPos) == null)
				MoveUnit(unit, newPos);
		}
		else
		{
			Model.GetHex(unit.CurrentPos).Occupants.Remove(unit);
			Model.GetHex(newPos).Occupants.Add(unit);
			unit.InvokeUpdateUnitPos(newPos);
		}
	}

	private const float DmgScale = 0.5f;
	private static void HandleCombat(UnitModel attacker, UnitModel defender, HexModel location)
	{
		float defenderDamage = DmgScale * (attacker.GetAttackValue() / defender.GetDefenseValue());
		defender.InvokeUpdateHP(defender.HealthCurr - defenderDamage);

		float attackerDamage = DmgScale * (defender.GetDefenseValue() / attacker.GetAttackValue());
		attacker.InvokeUpdateHP(attacker.HealthCurr - attackerDamage);
	}

	public static void ExecuteUnitAI()
	{
		foreach (HexModel hex in Model.AllHexes())
		{
			foreach (HexOccupier occupant in hex.Occupants.Copy())
			{
				if (occupant is UnitModel && !((UnitModel)occupant).Faction.PlayerControlAllowed())
					UnitAIHandler.ExecuteAIMove((UnitModel)occupant);
			}
		}
	}

	public static void RefreshUnitMovements()
	{
		foreach (HexModel hex in Model.AllHexes())
		{
			foreach (HexOccupier occupant in hex.Occupants)
			{
				if (occupant is UnitModel)
					((UnitModel)occupant).MovementCurr = ((UnitModel)occupant).MovementMax;
			}
		}
	}

	public static void RemoveUnit(UnitModel unitToRemove)
	{
		Model.GetHex(unitToRemove.CurrentPos).Occupants.Remove(unitToRemove);
	}
}

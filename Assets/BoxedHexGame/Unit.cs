using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
	//Attack/Defense Code
	//TODO: Modify this system as needed:
	[SerializeField] private float AttackStrength = 1;
	[SerializeField] private float DefenseStrength = 1;
	[SerializeField] private float MaxHP = 5;
	[SerializeField] private float CurrHP = 5;

	//AI determines if it wants to bother attacking a unit
	public float AiCalcDesireToAttackNode(Node node)
	{
		return 1 / (node.GetEntryAttackCost(CurrNode) + node.CurrentOccupant.DefenseStrength);
	}

	public float AiCalcDesireToMoveToNode(Node node)
	{
		return node.GetEntryAttackCost(CurrNode);
	}

	//This unit attacks an enemy unit on it's own node
	public void EngageEnemy(float nodeDefensiveValue, Unit enemy)
	{
		var defenderStrength = enemy.DefenseStrength + nodeDefensiveValue;
		TakeDamage(defenderStrength/2);
		enemy.TakeDamage(AttackStrength);
	}

	//This unit takes damage and possibly destroys itself
	public void TakeDamage(float damage)
	{
		CurrHP -= damage;
		if (CurrHP <= 0)
		{
			CurrNode.CurrentOccupant = null;
			Faction.Units.Remove(this);
			Destroy(gameObject);
		}
	}
	//End Attack/Defense Code

	public Faction Faction;
	public UnitVisuals Visuals;

	[SerializeField] public Node CurrNode;

	[SerializeField] private float MaxMovePoints;
	[SerializeField] public float CurrMovePoints;
	
	public void MoveTo(KeyValuePair<Node, float> dest)
	{
		if (dest.Key == null)
			return;

		CurrNode.CurrentOccupant = null;
		dest.Key.CurrentOccupant = this;

		CurrMovePoints -= dest.Value;

		dest.Key.UpdateOwner(Faction);
		Visuals.MoveToNode(dest.Key);
	}

	public void Attack(Node nodeToAttack)
	{
		if (nodeToAttack?.CurrentOccupant == null)
			return;

		EngageEnemy(nodeToAttack.GetEntryAttackCost(CurrNode), nodeToAttack.CurrentOccupant);

		if(nodeToAttack.CurrentOccupant == null && CurrHP > 0)
			MoveTo(new KeyValuePair<Node, float>(nodeToAttack, nodeToAttack.GetEntryMoveCost(CurrNode, this)));
	}

	public void Refresh()
	{
		CurrMovePoints = MaxMovePoints;
	}
}

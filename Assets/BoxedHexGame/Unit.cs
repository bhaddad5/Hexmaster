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

	public void EngageEnemy(float nodeDefensiveValue, Unit enemy)
	{
		var defenderStrength = enemy.DefenseStrength + nodeDefensiveValue;
		TakeDamage(defenderStrength/2);
		enemy.TakeDamage(AttackStrength);
	}

	public void TakeDamage(float damage)
	{
		CurrHP -= damage;
		if (CurrHP <= 0)
		{
			Node.CurrentOccupant = null;
			Faction.Units.Remove(this);
			Destroy(gameObject);
		}
	}
	//End Attack/Defense Code

	public Faction Faction;

	[SerializeField] private Node Node;

	[SerializeField] private float MaxMovePoints;
	[SerializeField] private float CurrMovePoints;
	
	public KeyValuePair<Node, float> GetAiMove()
	{
		KeyValuePair<Node, float> desiredMovePath = new KeyValuePair<Node, float>();
		float desireToFollowPath = 0;
		foreach (var possibleMovePath in GetPossibleMoves())
		{
			var desire = CalcDesireToMoveToNode(possibleMovePath);
			if (desire > desireToFollowPath)
			{
				desiredMovePath = possibleMovePath;
				desireToFollowPath = desire;
			}
		}
		return desiredMovePath;
	}

	public Node GetAiAttckNode()
	{
		Node desiredAttackNode = null;
		float desireToAttackNode = 0;
		foreach (Node possibleAttackNode in GetPossibleAttackNodes())
		{
			var desire = CalcDesireToAttackNode(possibleAttackNode);
			if (desire > desireToAttackNode)
			{
				desiredAttackNode = possibleAttackNode;
				desireToAttackNode = desire;
			}
		}
		return desiredAttackNode;
	}

	private float CalcDesireToMoveToNode(KeyValuePair<Node, float> node)
	{
		return 0;
	}

	private float CalcDesireToAttackNode(Node node)
	{
		return 0;
	}

	public Dictionary<Node, float> GetPossibleMoves()
	{
		Dictionary<Node, float> possibleMoves = new Dictionary<Node, float>();

		SortedDupList<Node> moveFrontier = new SortedDupList<Node>();
		moveFrontier.Insert(Node, CurrMovePoints);

		while (moveFrontier.Count > 0)
		{
			Node currNode = moveFrontier.TopValue();
			float currMovePtsRemaining = moveFrontier.TopKey();
			possibleMoves[currNode] = currMovePtsRemaining;
			if (currNode.CurrentOccupant == null || currNode.CurrentOccupant.Faction == Faction)
			{
				foreach (Node neighbor in currNode.Neighbors)
				{
					if (neighbor.GetEntryMoveCost(currNode, this) >= 0 && !moveFrontier.ContainsValue(neighbor) &&
						!possibleMoves.ContainsKey(neighbor) &&
						currMovePtsRemaining - neighbor.GetEntryMoveCost(currNode, this) >= 0)
					{
						if (currNode.BordersEnemy(Faction) && !neighbor.ContainsEnemy(Faction) && !neighbor.ContainsAlly(Faction) && neighbor.BordersEnemy(Faction))
							continue;
						if (currNode.ContainsAlly(Faction) && currNode != this && neighbor.ContainsEnemy(Faction))
							continue;
						moveFrontier.Insert(neighbor, currMovePtsRemaining - neighbor.GetEntryMoveCost(currNode, this));
					}
				}
			}
			moveFrontier.Pop();
		}

		List<Node> hexesWithUnits = possibleMoves.Keys.Where(n => n.CurrentOccupant != null).ToList();
		foreach (Node hexToRemove in hexesWithUnits)
			possibleMoves.Remove(hexToRemove);

		return possibleMoves;
	}

	public List<Node> GetPossibleAttackNodes()
	{
		List<Node> nodes = new List<Node>();
		foreach (Node neighbor in Node.Neighbors)
		{
			if(neighbor != null && neighbor.ContainsEnemy(Faction))
				nodes.Add(neighbor);
		}
		return nodes;
	}

	public void MoveTo(KeyValuePair<Node, float> dest)
	{
		if (dest.Key == null)
			return;

		Node.CurrentOccupant = null;
		dest.Key.CurrentOccupant = this;

		CurrMovePoints -= dest.Value;
		//Move To View???
		transform.position = dest.Key.transform.position + new Vector3(0, 0, MoveHelpers.UnitHeight);
	}

	public void Attack(Node nodeToAttack)
	{
		if (nodeToAttack?.CurrentOccupant == null)
			return;

		EngageEnemy(nodeToAttack.GetEntryAttackCost(Node), nodeToAttack.CurrentOccupant);

		if(nodeToAttack.CurrentOccupant == null && CurrHP > 0)
			MoveTo(new KeyValuePair<Node, float>(nodeToAttack, nodeToAttack.GetEntryMoveCost(Node, this)));
	}

	public void Refresh()
	{
		CurrMovePoints = MaxMovePoints;
	}

	public Sprite EditorGraphic;
}

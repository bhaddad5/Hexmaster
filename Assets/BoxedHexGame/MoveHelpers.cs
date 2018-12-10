using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MoveHelpers
{
	public static float UnitHeight = .01f;

	public static List<Node> GetPossibleAttackNodes(Node startNode, Faction unitFaction)
	{
		List<Node> nodes = new List<Node>();
		foreach (Node neighbor in startNode.Neighbors)
		{
			if (neighbor != null && neighbor.ContainsEnemy(unitFaction))
				nodes.Add(neighbor);
		}
		return nodes;
	}

	public static KeyValuePair<Node, float> GetAiMove(Unit unit)
	{
		KeyValuePair<Node, float> desiredMovePath = new KeyValuePair<Node, float>();
		float desireToFollowPath = 0;
		foreach (var possibleMovePath in GetPossibleMoves(unit.CurrNode, unit.CurrMovePoints, unit, unit.Faction))
		{
			var desire = possibleMovePath.Key.GetDesireToEnter(unit);
			if (desire > desireToFollowPath)
			{
				desiredMovePath = possibleMovePath;
				desireToFollowPath = desire;
			}
		}
		return desiredMovePath;
	}

	public static Node GetAiAttckNode(Node startingNode, Faction faction, Unit unit)
	{
		Node desiredAttackNode = null;
		float desireToAttackNode = 0;
		foreach (Node possibleAttackNode in MoveHelpers.GetPossibleAttackNodes(startingNode, faction))
		{
			var desire = unit.AiCalcDesireToAttackNode(possibleAttackNode);
			if (desire > desireToAttackNode)
			{
				desiredAttackNode = possibleAttackNode;
				desireToAttackNode = desire;
			}
		}
		return desiredAttackNode;
	}

	public static Dictionary<Node, float> GetPossibleMoves(Node startingNode, float movePoints, Unit unit, Faction faction)
	{
		Dictionary<Node, float> possibleMoves = new Dictionary<Node, float>();

		SortedDupList<Node> moveFrontier = new SortedDupList<Node>();
		moveFrontier.Insert(startingNode, movePoints);

		while (moveFrontier.Count > 0)
		{
			Node currNode = moveFrontier.TopValue();
			float currMovePtsRemaining = moveFrontier.TopKey();
			possibleMoves[currNode] = currMovePtsRemaining;
			if (currNode.CurrentOccupant == null || currNode.CurrentOccupant.Faction == faction)
			{
				foreach (Node neighbor in currNode.Neighbors)
				{
					if (neighbor.GetEntryMoveCost(currNode, unit) >= 0 && 
						neighbor.NodePassable(currNode) &&
					    !moveFrontier.ContainsValue(neighbor) &&
					    !possibleMoves.ContainsKey(neighbor) &&
					    currMovePtsRemaining - neighbor.GetEntryMoveCost(currNode, unit) >= 0)
					{
						if (currNode.BordersEnemy(faction) && !neighbor.ContainsEnemy(faction) && !neighbor.ContainsAlly(faction) && neighbor.BordersEnemy(faction))
							continue;
						if (currNode.ContainsAlly(faction) && currNode != unit && neighbor.ContainsEnemy(faction))
							continue;
						moveFrontier.Insert(neighbor, currMovePtsRemaining - neighbor.GetEntryMoveCost(currNode, unit));
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
}

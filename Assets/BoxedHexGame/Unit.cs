using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
	//Attack/Defense Stats
	//TODO: Modify this system as needed:
	public float AttackStrength = 1;
	public float DefenseStrength = 1;
	public float HP = 5;
	//End Attack/Defense Stats

	[SerializeField] private float MovePoints;
	[SerializeField] private float CurrMovePoints;
	[SerializeField] private Faction Faction;

	[SerializeField] private Node Node;

	public Node[] GetAiMovePath()
	{
		Node[] desiredMovePath = null;
		float desireToFollowPath = 0;
		foreach (var possibleMovePath in Node.GetPossibleMovePaths(this))
		{
			var desire = CalcDesireToMoveToNode(possibleMovePath.ToList().Last());
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
		foreach (Node possibleAttackNode in Node.GetPossibleAttackNodes(this))
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

	private float CalcDesireToMoveToNode(Node node)
	{
		return 0;
	}

	private float CalcDesireToAttackNode(Node node)
	{
		return 0;
	}

	public void MoveTo(Node[] path)
	{

	}

	public void Attack(Node node)
	{

	}

	public void Refresh()
	{
		CurrMovePoints = MovePoints;
	}
}

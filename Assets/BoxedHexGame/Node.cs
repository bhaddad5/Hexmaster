using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdgeModifiers
{
	public NodeEdge[] EdgeMods;
}

public class Node : MonoBehaviour
{
	public bool Passable = true;
	public float EntryMoveCost;
	public float EntryAttackCost;

	public Node[] Neighbors = new Node[6];
	public EdgeModifiers[] Edges = new EdgeModifiers[6];

	public Unit CurrentOccupant;
	public Faction Owner;

	public float GetEntryMoveCost(Node fromNode)
	{
		var index = Neighbors.ToList().IndexOf(fromNode);
		float edgeCost = 0;
		foreach (NodeEdge edgeMod in Edges[index].EdgeMods)
		{
			edgeCost += edgeMod.EntryMoveCost;
		}
		return EntryMoveCost + edgeCost;
	}

	public float GetEntryAttackCost(Node fromNode)
	{
		var index = Neighbors.ToList().IndexOf(fromNode);
		float edgeCost = 0;
		foreach (NodeEdge edgeMod in Edges[index].EdgeMods)
		{
			edgeCost += edgeMod.EntryAttackCost;
		}
		return EntryAttackCost + edgeCost;
	}

	public Node[][] GetPossibleMovePaths(Unit unit)
	{

	}

	public Node[] GetPossibleAttackNodes(Unit unit)
	{

	}

	public Sprite MapEditorGraphic;
}

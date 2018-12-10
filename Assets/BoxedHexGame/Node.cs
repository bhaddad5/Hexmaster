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
	public NodeVisuals NodeVis;

	public Node[] Neighbors = new Node[6];
	public EdgeModifiers[] Edges = new EdgeModifiers[6];
	public NodeContents Contents;

	public Unit CurrentOccupant;
	public Faction Owner;

	//How badly do units want to move to this tile?
	//Fill in with more AI logic as you go.
	public float GetDesireToEnter(Unit unit)
	{
		float baseDesire = 0;
		baseDesire += Contents.EntryAttackCost;
		if (unit.Faction.Allies.Contains(Owner) && unit.Faction != Owner)
			baseDesire += 1;
		return baseDesire;
	}

	public bool NodePassable(Node fromNode)
	{
		var index = Neighbors.ToList().IndexOf(fromNode);
		foreach (NodeEdge edgeMod in Edges[index].EdgeMods)
		{
			if (!edgeMod.Passable)
				return false;
		}
		return Contents.Passable;
	}
	
	public float GetEntryMoveCost(Node fromNode, Unit unit)
	{
		var index = Neighbors.ToList().IndexOf(fromNode);
		float edgeCost = 0;
		foreach (NodeEdge edgeMod in Edges[index].EdgeMods)
		{
			edgeCost += edgeMod.EntryMoveCost;
		}
		return Contents.EntryMoveCost + edgeCost;
	}

	public float GetEntryAttackCost(Node fromNode)
	{
		var index = Neighbors.ToList().IndexOf(fromNode);
		float edgeCost = 0;
		foreach (NodeEdge edgeMod in Edges[index].EdgeMods)
		{
			edgeCost += edgeMod.EntryAttackCost;
		}
		return Contents.EntryAttackCost + edgeCost;
	}

	public bool ContainsEnemy(Faction faction)
	{
		if (CurrentOccupant != null && !CurrentOccupant.Faction.Allies.Contains(faction))
			return true;
		return false;
	}

	public bool ContainsAlly(Faction faction)
	{
		if (CurrentOccupant != null && (CurrentOccupant.Faction.Allies.Contains(faction) || CurrentOccupant.Faction == faction))
			return true;
		return false;
	}

	public bool BordersEnemy(Faction faction)
	{
		foreach (Node neighbor in Neighbors)
		{
			if (neighbor.CurrentOccupant != null && !neighbor.CurrentOccupant.Faction.Allies.Contains(faction))
				return true;
		}
		return false;
	}

	public void UpdateOwner(Faction newOwner)
	{
		Owner = newOwner;
		NodeVis.DisplayOwner();
	}
}

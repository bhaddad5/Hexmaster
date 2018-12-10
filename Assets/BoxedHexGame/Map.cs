using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Column
{
	public List<Node> Nodes;
}

public class Map : MonoBehaviour
{
	public MapVisuals Visuals;

	public List<Column> Columns;
}

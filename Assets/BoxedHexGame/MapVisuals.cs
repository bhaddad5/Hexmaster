using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisuals : MonoBehaviour
{
	public Map Map;
	
	public const float NodeXOffset = .84f;
	public const float NodeYOffset = .72f;

	public void DisplayMap()
	{
		for (int width = 0; width < Map.Columns.Count; width++)
		{
			for (int height = 0; height < Map.Columns[0].Nodes.Count; height++)
			{
				float evenOffset = height%2 == 0 ? NodeXOffset/2f : 0;
				Map.Columns[width].Nodes[height].NodeVis.DisplayNode(width * NodeXOffset + evenOffset, height * NodeYOffset);
			}
		}
	}
}

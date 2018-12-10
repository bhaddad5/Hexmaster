using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisuals : MonoBehaviour
{
	public Node Node;

	private const float NodeHeight = 0;

	public void DisplayOwner()
	{

	}

	public void DisplayNode(float x, float y)
	{
		transform.position = new Vector3(x, y, NodeHeight);

		Node.Contents.transform.localPosition = Vector3.zero;

		for (int i = 0; i < Node.Edges.Length; i++)
		{
			foreach (NodeEdge edgeMod in Node.Edges[i].EdgeMods)
			{
				edgeMod.transform.localPosition = Vector3.zero;
				edgeMod.transform.localEulerAngles = new Vector3(0, 0, -60 * i);
			}
		}
	}
}

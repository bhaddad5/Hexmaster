using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisuals : MonoBehaviour
{
	public Sprite EditorSprite;

	public void MoveToNode(Node node)
	{
		transform.position = node.transform.position + new Vector3(0, 0, MoveHelpers.UnitHeight);
	}
}
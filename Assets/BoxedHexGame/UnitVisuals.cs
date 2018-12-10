using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisuals : MonoBehaviour
{
	public Sprite EditorSprite;

	private const float UnitHeight = .01f;

	public void MoveToNode(Node node)
	{
		transform.position = node.transform.position + new Vector3(0, 0, -UnitHeight);
	}
}
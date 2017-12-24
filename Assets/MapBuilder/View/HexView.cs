using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexView : MonoBehaviour
{
	public HexModel HexModel;
	public SpriteRenderer SpriteView;

	void Start()
	{
		SpriteView.sprite = HexModel.Sprite;
	}
}

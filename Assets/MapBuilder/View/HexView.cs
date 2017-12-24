using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexView : MonoBehaviour
{
	public HexModel HexModel;

	public SpriteRenderer SpriteView;
	public Text CoordText;

	public Vector2 Coord;

	void Start()
	{
		SpriteView.sprite = HexModel.Sprite;
		CoordText.text = Coord.x + ", " + Coord.y;
	}
}

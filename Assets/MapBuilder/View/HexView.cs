using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexView : MonoBehaviour
{
	public HexModel HexModel;

	public Image Sprite;
	public Image HexBorder;
	public TMP_Text CoordText;

	void Start()
	{
		Sprite.sprite = HexModel.Sprite;
		CoordText.text = HexModel.Coord.X + ", " + HexModel.Coord.Z;
	}

	private void HighlightSelectable()
	{
		
	}
}

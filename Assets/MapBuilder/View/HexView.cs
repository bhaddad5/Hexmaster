using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractableObject { }

public class HexView : MonoBehaviour, IInteractableObject
{
	public HexModel HexModel;

	public Image Sprite;
	public Image HexBorder;
	public TMP_Text CoordText;

	void Start()
	{
		Sprite.sprite = HexModel.Sprite;
		CoordText.text = HexModel.Coord.X + ", " + HexModel.Coord.Z;

		HexModel.TriggerHighlight += HighlightHex;
		HighlightHex(HexModel.HexHighlightTypes.None);
	}

	private void HighlightHex(HexModel.HexHighlightTypes highlight)
	{
		if(highlight == HexModel.HexHighlightTypes.None)
			HexBorder.color = Color.black;
		if (highlight == HexModel.HexHighlightTypes.Move)
			HexBorder.color = Color.green;
		if (highlight == HexModel.HexHighlightTypes.Attack)
			HexBorder.color = Color.red;
	}

	public void ToggleCoordinates()
	{
		CoordText.gameObject.SetActive(!CoordText.gameObject.activeSelf);
	}
}

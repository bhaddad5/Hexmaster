using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetView : MonoBehaviour
{
	[HideInInspector]
	public PlanetModel Model;

	public TMP_Text PlanetName;

	// Use this for initialization
	void Start ()
	{
		PlanetName.text = Model.PlanetName;
		transform.position = MapInstantiator.GetHexWorldPos(Model.CurrentPos);
	}
}

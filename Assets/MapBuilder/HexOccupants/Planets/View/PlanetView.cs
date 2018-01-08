using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetView : MonoBehaviour
{
	[HideInInspector]
	public PlanetModel Model;

	public TMP_Text PlanetName;
	public Transform PlanetTransform;

	// Use this for initialization
	void Start ()
	{
		PlanetName.text = Model.PlanetName;

		PlanetTransform.localScale = new Vector3(Model.PlanetSize, Model.PlanetSize, Model.PlanetSize);

		transform.position = MapInstantiator.GetHexWorldPos(Model.CurrentPos);
	}
}

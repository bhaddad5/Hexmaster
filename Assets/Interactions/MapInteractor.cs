using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractor : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Transform objectHit = hit.transform;
				HexView result = objectHit.GetComponentInParent<HexView>();
				if (result != null)
				{
					Debug.Log("me: " + result.Coord);
					foreach (HexModel neighbor in result.HexModel.Neighbors)
					{
						Debug.Log(neighbor.Coord);
					}
				}
			}
		}
	}
}

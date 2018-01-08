using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	private float Speed = 10f;

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up, Speed * Time.deltaTime);
	}
}

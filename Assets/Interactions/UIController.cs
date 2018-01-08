using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public GameObject Planet;
	public GameObject Unit;

	private static GameObject PlanetUI;
	private static GameObject UnitUI;

	void Start()
	{
		PlanetUI = Planet;
		UnitUI = Unit;

		CloseAllUI();
	}

	public static void OpenPlanetUI(PlanetModel planet)
	{
		CloseAllUI();
		PlanetUI.SetActive(true);
	}

	public static void OpenUnitUI(UnitModel unit)
	{
		CloseAllUI();
		UnitUI.SetActive(true);
	}

	public static void CloseAllUI()
	{
		PlanetUI.SetActive(false);
		UnitUI.SetActive(false);
	}
}

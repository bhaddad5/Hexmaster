using UnityEngine;

public class SelectedPlanetController
{
	private PlanetModel CurrentSelectedPlanet;

	public void HandleNewSelectedPlanet(PlanetModel planet)
	{
		CurrentSelectedPlanet = planet;
		UIController.OpenPlanetUI(planet);
	}

	public void ViewSelectedPlanet()
	{
		Debug.Log("Viewing " + CurrentSelectedPlanet.PlanetName);
	}
}

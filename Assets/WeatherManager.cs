using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	enum Weather {
		SNOW, SUN, RAIN, SIZE
	}

	Weather weather;

	public Material sunSkybox;
	
	void Start () {
		weather = (Weather)Random.Range (0, (int)Weather.SIZE);

		switch (weather) {
		case Weather.SUN:
			RenderSettings.skybox = sunSkybox;
			break;
		case Weather.RAIN:
			break;
		default:
			print("Weather not implemented");
			break;
		}
	}
}

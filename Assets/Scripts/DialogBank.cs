using UnityEngine;
using System.Collections;

public class DialogBank : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static string IceBreaker(WeatherManager.Weather weather) {
		switch(weather) {
		case WeatherManager.Weather.RAIN:
			return "sure is rainy today!";
		case WeatherManager.Weather.SNOW:
			return "man, it's cold out!";
		case WeatherManager.Weather.SUN:
			return "nice day, huh?";
		default:
			return "";
		}
	}
}

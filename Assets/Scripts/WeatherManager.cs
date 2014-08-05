using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	static WeatherManager s_instance;

	public enum Weather {
		SNOW, SUN, RAIN, SIZE
	}

	Weather m_weather;

	public GameObject player;

	public Light sun;

	public Material sunSkybox;
	public Material snowSkyBox;
	public Material rainSkyBox;

	public GameObject snowEmitter;
	public GameObject rainEmitter;

	public Material snowMat;

	public AudioClip rainSound; 

	void Start () {
		s_instance = this;
		audio.Stop();
		m_weather = (Weather)Random.Range (0, (int)Weather.SIZE);

		switch (weather) {
		case Weather.SUN:
			RenderSettings.skybox = sunSkybox;
			break;
		case Weather.RAIN:
			RenderSettings.skybox = rainSkyBox;
			GameObject t_rainEmit = (GameObject)Instantiate(rainEmitter);
			t_rainEmit.transform.parent = player.transform;
			t_rainEmit.transform.position = Vector3.up * 8.0f;
			sun.flare = null;
			audio.clip = rainSound;
			audio.Play();
			break;
		case Weather.SNOW:
			RenderSettings.skybox = snowSkyBox;
			GameObject t_snowEmit = (GameObject)Instantiate(snowEmitter);
			t_snowEmit.transform.parent = player.transform;
			t_snowEmit.transform.position = Vector3.up * 8.0f;
			sun.flare = null;
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Scenery")) {
				Material t_mat = new Material(snowMat);
				t_mat.mainTexture = obj.renderer.material.mainTexture;
				obj.renderer.material = t_mat;
			}
			break;
		default:
			print("Weather not implemented");
			break;
		}
	}

	public static WeatherManager instance {
		get {
			return s_instance;
		}
	}

	public Weather weather {
		get {
			return m_weather;
		}
	}
}

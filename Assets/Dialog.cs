using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

	Transform player;

	public float talkDistance = 4.0f;

	bool playerNear = false;

	string message;

	string firstName;
	string lastName;

	public GUISkin skin;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		message = "Hey, " + DialogBank.IceBreaker(WeatherManager.instance.weather);
		firstName = NameBank.RandomName();
		do {
			lastName = NameBank.RandomName();
		}while(firstName == lastName);
	}

	void Update () {
		playerNear = Vector3.Distance(player.position, transform.position) < talkDistance;
		if(!UDP_RecoReciever.Get().wordUsed) {
			if(UDP_RecoReciever.Get().UDPGetPacket() == "Yes") {
				message = "Yes?";
				UDP_RecoReciever.Get().wordUsed = true;
			}
		}
	}

	void OnGUI() {
		if(playerNear) {
			GUILayout.BeginArea(new Rect(Screen.width/4.0f, Screen.height*.75f, Screen.width/2.0f, Screen.height/4.0f),GUI.skin.window );

			GUILayout.Label(firstName + " " + lastName, skin.customStyles[0]);
			GUILayout.Label(message, skin.label);
			GUILayout.EndArea();
		}
	}
}

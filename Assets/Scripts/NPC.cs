using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


/// <summary>
/// NPC contains and manages the AI behavior, character info, character rendering etc.
/// </summary>
public class NPC : MonoBehaviour {


    public Character character;


    void Start() {
        character.Initialize();
    }


	/*
	void Update () {
		label.text = "";
		for(int i = 0; i < needs.Count; i++) {
#if DEBUGMODE
			//print (needs[i].name + ": imp - " + needs[i].importance + ", str - " + needs[i].strength + ", total = " + needs[i].importance * needs[i].strength);
			label.text += (
				needs[i].name 
				+ "- imp: " + SetPrecision(needs[i].importance, 2)
				+ ", str: " + SetPrecision(needs[i].strength, 2) 
				+ ", total = " + SetPrecision(needs[i].importance * needs[i].strength, 2)
       			+ "\n"
       		);
#endif
			if(highestNeed != i) {
		    	if(needs[i].importance * needs[i].strength > needs[highestNeed].importance * needs[highestNeed].strength)
					highestNeed = i;
			}
		}
#if DEBUGMODE
		label.text += "Highest: " + needs[highestNeed].name;
		label.transform.rotation = Quaternion.LookRotation(-(Camera.main.transform.position - label.transform.position));
#endif
	
		UpdateNeeds();
	}

	void UpdateNeeds() {
		// Safety
		float personalBubble = 7.0f;
		float distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
		if(distance < personalBubble) {
			needs[2].strength = (1.0f - distance / personalBubble) * 10.0f;
	//		SetPrecision(ref needs[2].strength, 2);
		} else {
			needs[2].strength = 0f;
		}
		// End Safety

		// Hunger
		float hungerRaiseAmount = 0.3f * Time.deltaTime;
		needs[0].strength += hungerRaiseAmount;
		needs[0].strength = Mathf.Clamp(needs[0].strength, 0.0f, 10.0f);
	//	SetPrecision(ref needs[0].strength, 2);
		// End Hunger

		// Sleep
		float sleepRaiseAmount = 0.1f * Time.deltaTime;
		needs[1].strength += sleepRaiseAmount;
		needs[1].strength = Mathf.Clamp(needs[1].strength, 0.0f, 10.0f);
	//	SetPrecision(ref needs[1].strength, 2);
		// End Sleep
	}

	float SetPrecision(ref float f, int precision) {
		float exp = Mathf.Pow(10.0f, precision);
		f = Mathf.Round(f * exp) / exp;
		return f;
	}

	float SetPrecision(float f, int precision) {
		float exp = Mathf.Pow(10.0f, precision);
		f = Mathf.Round(f * exp) / exp;
		return f;
	}
     * */
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// NPC Behavior
/// </summary>
public class NPC : MonoBehaviour {
	
	
	public Dictionary<string, Need> needs;
	
	// Temproary while we get hte XML working
	public List<Need> tempNeeds;
	
	
	[System.Serializable]
	public class Need {
		
		public string name = "";
		
		/// <summary>
		/// Changes based on the actors around the NPC and any custom logic that is applied to this need.
		/// </summary>
		public float value = 0;
		
		public int priority = 0;
		
		/// <summary>
		/// What modefies the priority of this need.
		/// </summary>
		public List<Modifier> priorityModefiers;
		
		/// <summary>
		///  The actions the AI will take to accomidate the need.
		/// </summary>
		public List<Action> actions;
		
	}
	
	
	[System.Serializable]
	public class Modifier {
		
		/// <summary>
		/// The name of the need/parmater the modefier will get the value from.
		/// </summary>
		public string name;
		
		/// <summary>
		/// An animation curve that changes the priority value based on the modefier value. 
		/// Gives more precision and fine tuning cabablilities.
		/// </summary>
		public AnimationCurve value;	
	}
	
	
	[System.Serializable]
	public class Action {
		
		public string name;
		
		/// <summary>
		/// The methode name that will be called to complete the action.
		/// </summary>
		public string methodeName;
		
		/// <summary>
		/// Whether the action will be excecuted.
		/// </summary>
		public string priority;
		
	}

	void Start () {		
		needs.Add("Hunger", new Need());
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

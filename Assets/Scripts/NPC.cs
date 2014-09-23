using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public enum WantType {
	Hunger, Safety, Sleep, Socialization, SIZE
}

public enum SenseType {
	Smell, Sight, Touch, Hear
}

public class NPC : MonoBehaviour {

	[System.NonSerialized]
	public string firstName;
	[System.NonSerialized]
	public string lastName;
	
	public float sightRange = 12f;
	public float smellRange = 7f;
	public float hearRange = 10f;
	public float touchRange = 0.8f;
	
	Dictionary<WantType, int> wants;

	public List<Effector> affectedBy;

	WantType currentTask = WantType.SIZE;

    void Start() {
		wants = new Dictionary<WantType, int> ();
		for (int i = 0; i < (int)WantType.SIZE; i++) {
			wants.Add((WantType)i, 50);
		}

		firstName = NameBank.RandomName();
		do {
			lastName = NameBank.RandomName();
		} while(firstName == lastName);

		Scheduler.AddTask (Look, true);
		Scheduler.AddTask (Smell, true);
		Scheduler.AddTask (Hear, true);
		Scheduler.AddTask (Touch, true);
		Scheduler.AddTask (UpdateItemStats, true);
		Scheduler.AddTask (NextTask, true);
    }

	void Update() {

	}

	void NextTask() {
		WantType highestWant = (WantType)0;
		for (int i = 1; i < (int)WantType.SIZE; i++) {
			if(wants[(WantType)i] > wants[highestWant])
				highestWant = (WantType)i;
		}
		if(highestWant != currentTask) {
			currentTask = highestWant;
			switch (highestWant) {
				case WantType.Hunger:
					Scheduler.AddTask(FindFood, false);
					break;
				case WantType.Safety:
					Scheduler.AddTask(FindSafety, false);
					break;
				case WantType.Sleep:
					Scheduler.AddTask(FindRest, false);
					break;
				case WantType.Socialization:
					Scheduler.AddTask(FindSocialization, false);
					break;
				default: 
					break;
			}
		}
	}

	void FindFood() {

	}

	void FindSafety() {

	}

	void FindRest() {

	}

	void FindSocialization() {

	}
	void ApplyItemStats(Item item, SenseType sense) {
		for(int i = 0, n = item.effector.Length; i < n; i++) {
			if(item.effector[i].senseEffected == sense) {
				if(!affectedBy.Contains(item.effector[i])) {
					wants[item.effector[i].want] += item.effector[i].value;
					item.effector[i].coolDown += Time.time; //Gimicky. Fix?
					affectedBy.Add(item.effector[i]);
				}
			}
	   	}
	}

	void UpdateItemStats() {
		for (int i = 0; i < affectedBy.Count; i++) {
			if(affectedBy[i].coolDown > Time.time) {
				affectedBy.RemoveAt(i);
				i--;
			}
		}
	}

	void Look() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < sightRange) {
				if(Physics.Linecast(items[i].transform.position, transform.position)) {
					ApplyItemStats(items[i], SenseType.Sight);
				}
			}
		}
	}

	void Smell() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < smellRange) {
				ApplyItemStats(items[i], SenseType.Smell);
			}
		}
	}

	void Hear() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < hearRange) {
				ApplyItemStats(items[i], SenseType.Hear);
			}
		}
	}

	void Touch() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < touchRange) {
				ApplyItemStats(items[i], SenseType.Touch);
			}
		}
	}
}

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

	[System.Serializable]
	public class Effect {
		public Effect(Effector p_effector, float curTime) {
			effector = p_effector; coolDown = curTime + effector.coolDown;
		}
		public Effector effector;
		public float coolDown;
	}

	public class Memory {
		public Memory(Item p_item, float p_timeSeen, int p_importance) {
			item = p_item;
			timeSeen = p_timeSeen;
			importance = p_importance;
		}
		public Memory() { }
		public Item item;
		public float timeSeen;
		public int importance;
	}

	[System.NonSerialized]
	public string firstName;
	[System.NonSerialized]
	public string lastName;
	
	public float sightRange = 12f;
	public float smellRange = 7f;
	public float hearRange = 10f;
	public float touchRange = 0.8f;
	
	Dictionary<WantType, int> wants;
	
	public List<Effect> affectedBy;

	List<Memory> memories;

	WantType currentTask = WantType.SIZE;

	NavMeshAgent agent;

	public float shortTermMemory = 40f;

    void Start() {
		memories = new List<Memory>();
		agent = GetComponent<NavMeshAgent>();

		wants = new Dictionary<WantType, int> ();
		for (int i = 0; i < (int)WantType.SIZE; i++) {
			wants.Add((WantType)i, Random.Range(30, 70));
		}

		affectedBy = new List<Effect>();

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
		Scheduler.AddTask(UpdateText, true);
		Scheduler.AddTask(UpdateMemories, true);
    }

	void Update() {

	}

	void UpdateMemories() {
		for(int i = 0; i < memories.Count; i++) {
			if(Time.time > memories[i].timeSeen + shortTermMemory * memories[i].importance) {
				memories.RemoveAt(i);
			}
		}
	}

	void UpdateText() {
		TextMesh tMesh = GetComponentInChildren<TextMesh>();
		tMesh.text = "";
		Dictionary<WantType, int>.Enumerator enumerator = wants.GetEnumerator();
		while(enumerator.MoveNext()) {
			tMesh.text += enumerator.Current.Key.ToString() + " " + enumerator.Current.Value + "\n";
		}
		tMesh.transform.LookAt(GameObject.Find("Player").transform.position);
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
		Item nearestFood = null;
		foreach(Memory memory in memories) {
			foreach(Effector effector in memory.item.effectors) {
				if(effector.want == WantType.Hunger && effector.senseEffected == SenseType.Touch && effector.value > 0) {
					if(nearestFood == null)
						nearestFood = memory.item;
					else {
						if(Vector3.Distance(memory.item.transform.position, transform.position) < Vector3.Distance(nearestFood.transform.position, transform.position)) {
							nearestFood = memory.item;
						}
					}
				}
			}
		}
		if(nearestFood != null) {
			agent.SetDestination(nearestFood.transform.position);
		}
	}

	void FindSafety() {

	}

	void FindRest() {
		Item nearestRest = null;
		foreach(Memory memory in memories) {
			foreach(Effector effector in memory.item.effectors) {
				if(effector.want == WantType.Sleep && effector.senseEffected == SenseType.Touch && effector.value > 0) {
					if(nearestRest == null)
						nearestRest = memory.item;
					else {
						if(Vector3.Distance(memory.item.transform.position, transform.position) < Vector3.Distance(nearestRest.transform.position, transform.position)) {
							nearestRest = memory.item;
						}
					}
				}
			}
		}
		if(nearestRest != null) {
			agent.SetDestination(nearestRest.transform.position);
		}
	}

	void FindSocialization() {
		NPC[] npcs = GameObject.FindObjectsOfType<NPC>();

		GameObject closestNPC = null;
		foreach(NPC npc in npcs) {
			if(npc == this)
				continue;
			if(closestNPC == null) {
				closestNPC = npc.gameObject;
			} else {
				if(Vector3.Distance(transform.position, npc.transform.position) < Vector3.Distance(transform.position, closestNPC.transform.position)) {
					closestNPC = npc.gameObject;
				}
			}
		}
		agent.SetDestination(closestNPC.transform.position);
	}

	void ApplyItemStats(Item item, SenseType sense) {
		for(int i = 0, n = item.effectors.Length; i < n; i++) {
			if(item.effectors[i].senseEffected == sense) {

				for(int j = 0, n2 = affectedBy.Count; j < n2; j++) {
					if(affectedBy[j].effector == item.effectors[i]) {
						return;
					}
				}

				wants[item.effectors[i].want] += item.effectors[i].value;
				affectedBy.Add(new Effect(item.effectors[i], Time.time));
			}
	   	}
	}

	void UpdateItemStats() {
		for(int i = 0; i < affectedBy.Count; i++) {
			if(Time.time > affectedBy[i].coolDown) {
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
					memories.Add(new Memory(items[i], Time.time, 1)); //TODO importance
				}
			}
		}
	}

	void Smell() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < smellRange) {
				ApplyItemStats(items[i], SenseType.Smell);
				memories.Add(new Memory(items[i], Time.time, 1)); //TODO importance	
			}
		}
	}

	void Hear() {
		List<Item> items = ItemManager.items;
		for(int i = 0, n = items.Count; i < n; i++) {
			if(Vector3.Distance(items[i].transform.position, transform.position) < hearRange) {
				ApplyItemStats(items[i], SenseType.Hear);
				memories.Add(new Memory(items[i], Time.time, 1)); //TODO importance
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

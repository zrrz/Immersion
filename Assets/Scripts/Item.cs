using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour{
	
	public Effector[] effector;

	void Start () {
		ItemManager.items.Add (this);
	}

	void Update () {
	
	}
}

[System.Serializable]
public class Effector {
	public WantType want;
	public SenseType senseEffected;
	public int value;
	public float coolDown;
}
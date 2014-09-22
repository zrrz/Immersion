using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour{
	
	public Effector[] effector;

	void Start () {

	}

	void Update () {
	
	}
}

[System.Serializable]
public class Effector {
	
	public Want want;
	public SenseEffected senseEffected;
	public int value;
}

public enum Want {
	Hunger, Safety, Sleep
}
public enum SenseEffected {
	Smell, Sight, Touch, Proximity
}
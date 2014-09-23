using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

	public static List<Item> items;

	void Awake() {
		items = new List<Item> ();
	}
}
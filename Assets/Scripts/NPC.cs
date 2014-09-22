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




    public float tickRate;

    void Start() {
        character.Initialize();
    }


	void Update() {
		Look ();

	}

	void Look() {

	}

}

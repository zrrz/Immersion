using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


/// <summary>
/// Responsible for handling the wants, needs of the NPC character as well as it's priorities.
/// </summary>
[System.Serializable]
public class Character {


    //[XmlAttribute("name")]
    public string firstName = "", middleName = "", lastName = "";


	//public Dictionary<string, Need> needs;
	

    [XmlArray("Needs"), XmlArrayItem("Need")]
	public List<Need> inspectorNeeds;
	
	

	[System.Serializable]
	public class Need {
		
        [XmlAttribute("name")]
		public string name = "";
		
		/// <summary>
		/// Changes based on the actors around the Character and any custom logic that is applied to this need.
		/// </summary>
        public float value = 0;
		
		public int priority = 0;
		
		/// <summary>
		/// What modefies the priority of this need.
		/// </summary>
        [XmlArray("Modifiers"), XmlArrayItem("Modifier")]
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
        [XmlAttribute("name")]
        public string name;
		
		/// <summary>
		/// An animation curve that changes the priority value based on the modefier value. 
		/// Gives more precision and fine tuning cabablilities.
		/// </summary>
		public AnimationCurve value;	
	}
	
	
	[System.Serializable]
	public class Action {

        [XmlAttribute("name")]
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
		//needs.Add("Hunger", new Need());
	}




    /// <summary>
    /// Saves the class to a XML format file at path.
    /// </summary>
    /// <param name="path"></param>
    [ContextMenu("Save")]
    public void Save(string path) {
        var serializer = new XmlSerializer(typeof(Character));
        using (var stream = new FileStream(path, FileMode.Create)) {
            serializer.Serialize(stream, this);
        }  
    }

    /// <summary>
    /// Load the class 
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Character</returns>
    public static Character Load(string path) {
        var serializer = new XmlSerializer(typeof(Character));
        using (var stream = new FileStream(path, FileMode.Open)) {
            return serializer.Deserialize(stream) as Character;
        }
    }

    public static Character LoadFromText(string text) {
        var serializer = new XmlSerializer(typeof(Character));
        return serializer.Deserialize(new StringReader(text)) as Character;
    }
}

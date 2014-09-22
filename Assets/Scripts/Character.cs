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

    [XmlIgnore]
    public Dictionary<string, Stat> stats;


    [XmlIgnore]
	public Dictionary<string, Need> needs;

    [XmlIgnore]
    public Dictionary<string, Want> wants;



    [XmlArray("Stats"), XmlArrayItem("Stat")]
    public List<Stat> inspectorStats;

    [XmlArray("Needs"), XmlArrayItem("Need")]
    public List<Need> inspectorNeeds;

    [XmlArray("Wants"), XmlArrayItem("Want")]
    public List<Want> inspectorWants;


    [System.Serializable]
    public class Stat
    {
        public string name;
        public float value = 0;
    }


	[System.Serializable]
	public class Need {
		
        [XmlAttribute("name")]
		public string name = "";
		
		public int priority = 0;
		
		/// <summary>
		/// What modifies the priority of this need.
		/// </summary>
        [XmlArray("Modifiers"), XmlArrayItem("Modifier")]
        public List<PriorityModifier> priorityModifiers;
		
		/// <summary>
		///  The actions the AI will take to accomidate the need.
		/// </summary>
        [XmlArray("Actiosn"), XmlArrayItem("Action")]
        public List<Action> actions;
		
	}

    [System.Serializable]
    public class Want
    {

        [XmlAttribute("name")]
        public string name = "";

        public int priority = 0;

        /// <summary>
        /// What modifies the priority of this need.
        /// </summary>
        [XmlArray("Modifiers"), XmlArrayItem("Modifier")]
        public List<PriorityModifier> priorityModifiers;

        /// <summary>
        ///  The actions the AI will take to accomidate the need.
        /// </summary>
        [XmlArray("Actions"), XmlArrayItem("Action")]
        public List<Action> actions;

    }
    
    [System.Serializable]
	public class PriorityModifier {
		
		/// <summary>
		/// The name of the need/parmater the modifier will get the value from.
		/// </summary>
        [XmlAttribute("name")]
        public string name;
		
		/// <summary>
		/// An animation curve that changes the priority value based on the modifier value. 
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


    public void Initialize()
    {

        stats = new Dictionary<string, Stat>();
        needs = new Dictionary<string, Need>();
        wants = new Dictionary<string, Want>();

        // Temporary:
        // Load the values from the inspector into the actial dictionary for use.
        for (int i = 0, n = inspectorStats.Count; i < n; i++)
        {
            stats.Add(inspectorStats[i].name, inspectorStats[i]);
        }

        for (int i = 0, n = inspectorNeeds.Count; i < n; i++)
        {
            needs.Add(inspectorNeeds[i].name, inspectorNeeds[i]);
        }

        for (int i = 0, n = inspectorWants.Count; i < n; i++)
        {
            wants.Add(inspectorWants[i].name, inspectorWants[i]);
        }

	}


    public void CalculateStats()
    {

        stats["Hunger"].value -= 1;
        stats["Thirst"].value -= 1;
        stats["Rest"].value -= 1;
        stats["Hunger"].value -= 1;
        stats["Hunger"].value -= 1;

    }

    public void CalculateNeeds()
    {

    }

    public void CalculateWants()
    {

    }


}

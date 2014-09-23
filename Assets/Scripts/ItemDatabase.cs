using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlType("Effect")]
public class EffectorXML {	
	public EffectorXML () {
//		want = new Want();
//		senseEffected = SenseEffected.Distance;
		value = 0f;
	}

	[XmlElement("Want")]
	public WantType want;
	[XmlElement("Sense")]
	public SenseType senseEffected;
	[XmlElement("Value")]
	public float value;
}

[XmlType("Item")]
public class ItemXML {
	public ItemXML () {
		effects = new List<EffectorXML> ();
	
		name = "item";
	}

	public string name;

	[XmlArray("Effects"), XmlArrayItem("EffectorXML")]
	public List<EffectorXML> effects;

	public void AddEffect() {
		effects.Add (new EffectorXML ());
	}
}

[XmlRoot("ItemDatabase")]
public class ItemDatabase {

	public ItemDatabase () {
		items = new List<ItemXML> ();
	}

	[XmlArray("Items"), XmlArrayItem("ItemXML")]
	public List<ItemXML> items;

	[System.NonSerialized]
	public static string path = "Items.xml";

	public void Save() {
		var serializer = new XmlSerializer(typeof(ItemDatabase));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static ItemDatabase Load() {
		var serializer = new XmlSerializer(typeof(ItemDatabase));
		if(!File.Exists(path)) {
			Debug.Log("\"" + path + "\" not found. Creating \"" + path + "\"");
			return new ItemDatabase();
		} else {
			using(var stream = new FileStream(path, FileMode.Open))
			{
				Debug.Log ("here");
				return serializer.Deserialize(stream) as ItemDatabase;
			}
		}
	}

	public void NewItem() {
		items.Add(new ItemXML());
	}
	
//	public static ItemDatabase LoadFromText(string text) {
//		var serializer = new XmlSerializer(typeof(ItemDatabase));
//		return serializer.Deserialize(new StringReader(text)) as ItemDatabase;
//	}
}

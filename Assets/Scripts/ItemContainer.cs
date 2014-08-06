using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class ItemXML {
	[XmlAttribute("name")]
	public string name = "item";

	//[XmlAttribute("need")]
	public string need = "need";

	public float effect = 0f;
}

[XmlRoot("ItemCollection")]
public class ItemContainer
{
	[XmlArray("Items"), XmlArrayItem("ItemXML")]
	public ItemXML[] items;

	public void Save(string path) {
		var serializer = new XmlSerializer(typeof(ItemContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static ItemContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(ItemContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ItemContainer;
		}
	}
	
	public static ItemContainer LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(ItemContainer));
		return serializer.Deserialize(new StringReader(text)) as ItemContainer;
	}
}

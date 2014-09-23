using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("WantsList")]
public class WantsList : XMLBase {


	override protected void Init() {
		wants = new List<string>();
		base.Init ();
	} 

	[XmlArray("Wants"), XmlArrayItem("string")]
	public List<string> wants;
}
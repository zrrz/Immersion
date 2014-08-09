using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


[System.Serializable]
public class Action {

    public string name;

    [XmlArray("StartStatModefs"), XmlArrayItem("StartStatMod")]
    public List<StatModefier> startActionStatModefiers;
   [XmlArray("TicktStatMods"), XmlArrayItem("TickStatMod")]
    public List<StatModefier> tickActionStatModefiers;
    [XmlArray("EndStatMods"), XmlArrayItem("EndStatMod")]
    public List<StatModefier> endActionStatModefiers;

    [System.Serializable]
    public class StatModefier
    {
        public string name;
        public float value, min, max;
    }

}

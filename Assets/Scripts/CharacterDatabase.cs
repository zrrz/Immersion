using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("CharacterDatabase")]//XmlType("CharacterDatabase")
public class CharacterDatabase
{
    [XmlArray("Characters"), XmlArrayItem("Character")] 
    public Character[] characters = new Character[1];


    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(CharacterDatabase));
        using (var stream = new FileStream(Path.Combine(Application.persistentDataPath, "CharacterDatabase"), FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static CharacterDatabase Load(string path)
    {
        var serializer = new XmlSerializer(typeof(CharacterDatabase));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as CharacterDatabase;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static CharacterDatabase LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(CharacterDatabase));
        return serializer.Deserialize(new StringReader(text)) as CharacterDatabase;
    }
}
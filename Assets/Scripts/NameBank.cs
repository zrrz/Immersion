using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class NameBank : MonoBehaviour {

	static NameBank s_instance;

	string fileName = "names.txt";

	List<string> m_names = new List<string>();
	
	void Awake () {
		s_instance = this;
		StreamReader sr = new StreamReader(Application.dataPath + "/" + fileName);
		string contents = sr.ReadToEnd();
		sr.Close();

		string[] lines = contents.Split("\n"[0]);
		foreach(string line in lines) {
			m_names.Add(line);
		}
	}

	public static string RandomName() {
		return instance.names[Random.Range(0, instance.names.Count)];
	}

	public static NameBank instance {
		get {
			return s_instance;
		}
	}

	public List<string> names {
		get {
			return m_names;
		}
	}

}

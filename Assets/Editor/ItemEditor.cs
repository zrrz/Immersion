using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {

	int index = 0;
	string[] options = new string[]{ "Cool", "Great", "Awesome" };
	
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		
		Rect r = EditorGUILayout.BeginHorizontal();
		index = EditorGUILayout.Popup("Awesome Drop down:", 
		                              index, options, EditorStyles.popup);
		EditorGUILayout.EndHorizontal();
	}
}

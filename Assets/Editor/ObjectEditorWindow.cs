using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObjectEditorWindow : EditorWindow {

	ItemContainer container;

	string path = "items.xml";

	[MenuItem("Window/Object Editor")]
	public static void ShowWindow() {
		EditorWindow.GetWindow (typeof(ObjectEditorWindow));
	}

	void OnGUI() {
		if(GUILayout.Button("Load")) {
			container = ItemContainer.Load(path);
		}

		if(GUILayout.Button("Save")) {
	//	if(GUI.changed) {
			container.Save(path);
		}

		if(container != null) {
		//	EditorGUILayout.BeginVertical ();
			for (int i = 0, n = container.items.Length; i < n; i++) {
				EditorGUILayout.BeginHorizontal();
				container.items[i].name = EditorGUILayout.TextField("Name", container.items[i].name);
				container.items[i].need = EditorGUILayout.TextField("Need", container.items[i].need);
				container.items[i].effect = EditorGUILayout.FloatField("Effect", container.items[i].effect);
				EditorGUILayout.EndHorizontal();
			}
		//	EditorGUILayout.EndVertical ();
		}
//		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
//		myString = EditorGUILayout.TextField ("Text Field", myString);
//		
//		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
//		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
//		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
//		EditorGUILayout.EndToggleGroup ();
	}
}

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObjectEditorWindow : EditorWindow {

	ItemDatabase container;

	[MenuItem("Immersion/Object Editor")]
	public static void ShowWindow() {
		EditorWindow.GetWindow (typeof(ObjectEditorWindow), false, "Object Editor", false);
	}

	void OnGUI() {
		if(GUILayout.Button("Load")) {
			container = ItemDatabase.Load();
		}

		if(GUILayout.Button("Save")) {
			container.Save();
		}

		if(container != null) {
		//	EditorGUILayout.BeginVertical ();
			Debug.Log(container.items.Count);
			for (int i = 0, n = container.items.Count; i < n; i++) {
				EditorGUILayout.BeginHorizontal();
				container.items[i].name = EditorGUILayout.TextField(container.items[i].name);
				EditorGUILayout.BeginVertical();
				for (int j = 0, n2 = container.items[i].effects.Count; j < n2; j++) {
					container.items[i].effects[j].senseEffected = (SenseEffected) EditorGUILayout.EnumPopup(container.items[i].effects[j].senseEffected);
					container.items[i].effects[j].want = (Want) EditorGUILayout.EnumPopup(container.items[i].effects[j].want);
					container.items[i].effects[j].value = EditorGUILayout.FloatField(container.items[i].effects[j].value);
				//	GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndVertical();
				if(GUILayout.Button("Add Effect", GUILayout.ExpandWidth(false))) {
					container.items[i].AddEffect();
				}
//				container.items[i].name = EditorGUILayout.TextField("Name", container.items[i].name, GUILayout.ExpandWidth(false));
//				container.items[i].need = EditorGUILayout.TextField("Need", container.items[i].need);
//				container.items[i].effect = EditorGUILayout.FloatField("Effect", container.items[i].effect);
				EditorGUILayout.EndHorizontal();
			}
		
			GUILayout.Space (10);
			
			if(GUILayout.Button("New Item", GUILayout.ExpandWidth(false))) {
				container.NewItem();
			}
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

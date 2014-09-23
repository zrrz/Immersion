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
			for (int i = 0, n = container.items.Count; i < n; i++) {
				if (EditorGUITools.DrawHeader(container.items[i].name, container.items[i].name)){
					EditorGUILayout.BeginHorizontal();
					container.items[i].name = EditorGUILayout.TextField(container.items[i].name);
					EditorGUILayout.BeginVertical();
					for (int j = 0, n2 = container.items[i].effects.Count; j < n2; j++) {
						container.items[i].effects[j].senseEffected = (SenseEffected) EditorGUILayout.EnumPopup(container.items[i].effects[j].senseEffected);
//						container.items[i].effects[j].want = EditorGUILayout.Popup(0, WantsList.Load<WantsList>());
						container.items[i].effects[j].value = EditorGUILayout.FloatField(container.items[i].effects[j].value);
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
			}

			GUILayout.Space (10);
			
			if(GUILayout.Button("New Item", GUILayout.ExpandWidth(false))) {
				container.NewItem();
			}
		}
	}
}

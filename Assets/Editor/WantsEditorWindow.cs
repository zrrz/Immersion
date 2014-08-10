using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class WantsEditorWindow : EditorWindow {

	WantsList wantsList;

	[MenuItem("Immersion/Wants")]
	public static void ShowWindow() {
		EditorWindow.GetWindow (typeof(WantsEditorWindow), false, "Wants", true);
	}

	void OnGUI() {
		if(null == wantsList) {
			wantsList = WantsList.Load<WantsList>();
			wantsList.Save();
		}

		GUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins);
		GUILayout.Label("Wants", EditorStyles.boldLabel);

		for(int i = 0; i < wantsList.wants.Count; i++) {
			GUILayout.BeginHorizontal();

			wantsList.wants[i] = GUILayout.TextField(wantsList.wants[i]);

			if(GUILayout.Button("-", EditorStyles.miniButton, GUILayout.ExpandWidth(false))) {
				wantsList.wants.RemoveAt(i);
			}

			GUILayout.EndHorizontal();
		}

		if(GUILayout.Button("+", GUILayout.ExpandWidth(false))) {
			wantsList.wants.Add("New Want");
		}

		GUILayout.EndVertical();

		if(GUI.changed) {
			wantsList.Save();
		}
	}
}
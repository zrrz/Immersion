using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor {
	SerializedProperty needs;	
	
	bool showNeeds = true;

	void OnEnable() {
		needs = serializedObject.FindProperty("tempNeeds");
	}

	public override void OnInspectorGUI() {
		
		serializedObject.Update ();
		
		//GUILayout.Space(5);
			
		GUILayout.BeginVertical(EditorStyles.objectFieldThumb);
		
		// Needs
		GUILayout.BeginHorizontal();
		GUILayout.Space(15);
		showNeeds = EditorGUILayout.Foldout(showNeeds, "Needs (" + needs.arraySize + "):");
		if (GUILayout.Button("+", GUILayout.Width(25))) needs.InsertArrayElementAtIndex(needs.arraySize);
		GUILayout.EndHorizontal();		
		
		if (showNeeds) {			
			for (int i = 0, n = needs.arraySize; i < n; i++) {				
				GUILayout.BeginVertical(EditorStyles.objectFieldThumb);
				
				GUILayout.BeginHorizontal();
				
				SerializedProperty need = needs.GetArrayElementAtIndex(i);
				SerializedProperty needPriority = need.FindPropertyRelative("priority");
				SerializedProperty needName = need.FindPropertyRelative("name");
				SerializedProperty needValue = need.FindPropertyRelative("value");
				SerializedProperty needPriorityModefiers = need.FindPropertyRelative("priorityModefiers");
				
				EditorGUIUtility.labelWidth = 50;
				
				GUILayout.Label("Priority: " + needPriority.intValue);
				
				GUILayout.Label("Name:");
				needName.stringValue = GUILayout.TextField(needName.stringValue, GUILayout.MinWidth(75));
				GUILayout.Label("Value:");
				needValue.floatValue = EditorGUILayout.FloatField(needValue.floatValue, GUILayout.MinWidth(75));
				
				EditorGUIUtility.labelWidth = 0;
				
				if (GUILayout.Button("-", GUILayout.Width(25))) { 
					Debug.Log(i); needs.DeleteArrayElementAtIndex(i); 
				}
				
				GUILayout.EndHorizontal();	

				GUILayout.BeginHorizontal();
				
				GUILayout.Label("Priority Modifiers (" + needPriorityModefiers.arraySize + "):");
								
				GUILayout.EndHorizontal();
				
				GUILayout.EndVertical();
			}			
		}
		
		GUILayout.EndVertical();
		
		DrawDefaultInspector();
		
		serializedObject.ApplyModifiedProperties();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{
    SerializedProperty character, needs, firstName, middleName, lastName;
	
    void OnEnable()
    {
        character = serializedObject.FindProperty("character");
        needs = character.FindPropertyRelative("inspectorNeeds");
        firstName = character.FindPropertyRelative("firstName");
        middleName = character.FindPropertyRelative("middleName");
        lastName = character.FindPropertyRelative("lastName");
    }
	
    public override void OnInspectorGUI() {

		serializedObject.Update ();

        firstName.stringValue = EditorGUILayout.TextField("First Name", firstName.stringValue);
        middleName.stringValue = EditorGUILayout.TextField("Middle Name",  middleName.stringValue);
        lastName.stringValue = EditorGUILayout.TextField("Last Name", lastName.stringValue);

        GUILayout.Space(5f);
       
        if (EditorGUITools.DrawHeader("Needs (" + needs.arraySize + "):", "ShowNeeds")) 
        {
            GUILayout.BeginVertical(EditorStyles.numberField);
            GUILayout.Space(5);

            for (int i = 0, n = needs.arraySize; i < n; i++)
            {
                // Get the variables from the need
                SerializedProperty need = needs.GetArrayElementAtIndex(i);
                SerializedProperty needName = need.FindPropertyRelative("name");

                if (EditorGUITools.DrawHeader(needName.stringValue, needName.stringValue + "_menu"))
                {
                    SerializedProperty needPriority = need.FindPropertyRelative("priority");
                    SerializedProperty needValue = need.FindPropertyRelative("value");
                    SerializedProperty priorityModifiers = need.FindPropertyRelative("priorityModifiers");
                    SerializedProperty actions = need.FindPropertyRelative("actions");
				
                    GUI.color = new Color(0.4f, 0.4f, 0.4f);
                    GUILayout.BeginVertical(EditorStyles.numberField);
                    GUI.color = Color.white;

                    GUILayout.Space(10);

                    // Name, value, priority
                    GUILayout.BeginHorizontal();

                    GUILayout.Label("Name:");
                    needName.stringValue = GUILayout.TextField(needName.stringValue, GUILayout.MinWidth(75));

                    GUILayout.Label("Value:");
                    needValue.floatValue = EditorGUILayout.FloatField(needValue.floatValue, GUILayout.MinWidth(75));

                    GUILayout.Label("Priority: " + needPriority.intValue);

                    if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(15))) { needs.DeleteArrayElementAtIndex(i); i = n; continue; }

                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);


                    // Show modifiers
                    if (EditorGUITools.DrawHeader("Priority Modifiers (" + priorityModifiers.arraySize + "):", needName.stringValue + "_priority"))
                    {
                        GUILayout.BeginVertical(EditorStyles.numberField);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Name:");
                        GUILayout.Label("Value:");
                        GUILayout.EndHorizontal();

                        for (int k = 0, m = priorityModifiers.arraySize; k < m; k++)
                        {

                            SerializedProperty modifier = priorityModifiers.GetArrayElementAtIndex(k);
                            SerializedProperty modifierName = modifier.FindPropertyRelative("name");
                            SerializedProperty modifierValue = modifier.FindPropertyRelative("value");

                            // Name, value, delete
                            GUILayout.BeginHorizontal();
							            
                            modifierName.stringValue = GUILayout.TextField(modifierName.stringValue, GUILayout.MinWidth(75));
                            
                            modifierValue.animationCurveValue = EditorGUILayout.CurveField(modifierValue.animationCurveValue);

                            if (GUILayout.Button("X", GUILayout.Width(25))) { priorityModifiers.DeleteArrayElementAtIndex(k); k = m; }

                            GUILayout.EndHorizontal();
                        }

                        if (GUILayout.Button("Add Modifier", EditorStyles.toolbarButton)) priorityModifiers.InsertArrayElementAtIndex(priorityModifiers.arraySize);

                        GUILayout.EndVertical();
                    }
				
                    GUILayout.Space(5);

                    // Show Actions TODO
                    if (EditorGUITools.DrawHeader("Actions(" + actions.arraySize + "):", needName.stringValue + "_actions"))
                    {

                    }

                    GUILayout.EndVertical();

                    if (i != n - 1)
                        GUILayout.Space(10);

                }

            }


            GUILayout.Space(5);
            if (GUILayout.Button("Add Need", EditorStyles.toolbarButton)) needs.InsertArrayElementAtIndex(needs.arraySize);
  
            GUILayout.EndVertical();
        }



        GUILayout.Space(5f);

        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Save To Database", EditorStyles.miniButtonLeft)) 
       // if (GUILayout.Button("Add To Database", EditorStyles.miniButtonLeft)) //
        //if (GUILayout.Button("Load from Database", EditorStyles.miniButtonMid)) //
       // GUILayout.EndHorizontal();

        
        serializedObject.ApplyModifiedProperties();
    }

}

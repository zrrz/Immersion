using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{

    SerializedProperty character, needs;



    // Use this for initialization
    void OnEnable()
    {
        character = serializedObject.FindProperty("character");
        needs = character.FindPropertyRelative("inspectorNeeds");
    }

    
    // Update is called once per frame
    public override void OnInspectorGUI() {

        serializedObject.Update();

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
                    SerializedProperty priorityModefiers = need.FindPropertyRelative("priorityModefiers");
                    SerializedProperty actions = need.FindPropertyRelative("actions");


                    GUI.color = new Color(0.65f, 0.65f, 0.65f);
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

                    GUILayout.Space(5);


                    // Show modefiers
                    if (EditorGUITools.DrawHeader("Priority Modefiers (" + priorityModefiers.arraySize + "):", needName.stringValue + "_priority"))
                    {

                        GUILayout.BeginVertical(EditorStyles.numberField);


                        for (int k = 0, m = priorityModefiers.arraySize; k < m; k++)
                        {

                            SerializedProperty modefier = priorityModefiers.GetArrayElementAtIndex(k);
                            SerializedProperty modefierNmae = modefier.FindPropertyRelative("name");
                            SerializedProperty modefierValue = modefier.FindPropertyRelative("value");

                            // Name, value, delete
                            GUILayout.BeginHorizontal();

                            GUILayout.Label("Name:");
                            modefierNmae.stringValue = GUILayout.TextField(modefierNmae.stringValue, GUILayout.MinWidth(75));
                            GUILayout.Label("Value:");
                            modefierValue.animationCurveValue = EditorGUILayout.CurveField(modefierValue.animationCurveValue);

                            if (GUILayout.Button("X", GUILayout.Width(25))) { priorityModefiers.DeleteArrayElementAtIndex(k); k = m; }

                            GUILayout.EndHorizontal();

                        }

                        if (GUILayout.Button("Add Modefier", EditorStyles.toolbarButton)) priorityModefiers.InsertArrayElementAtIndex(priorityModefiers.arraySize);

                        GUILayout.EndVertical();
                    }


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


        //DrawDefaultInspector();

        GUILayout.Space(5f);

        GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Save", EditorStyles.miniButtonLeft)) foreach(NPC npc in targets)  npc.Save("HungryBob.xml");
        //if (GUILayout.Button("Load", EditorStyles.miniButtonMid)) needs.InsertArrayElementAtIndex(needs.arraySize);
        //if (GUILayout.Button("Reset", EditorStyles.miniButtonRight)) needs.InsertArrayElementAtIndex(needs.arraySize);
        GUILayout.EndHorizontal();


        //DrawDefaultInspector();
        
        serializedObject.ApplyModifiedProperties();
    }

}

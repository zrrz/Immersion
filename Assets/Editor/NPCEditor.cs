using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{

    //SerializedProperty character, needs, wants, firstName, middleName, lastName;



    // Use this for initialization
    void OnEnable()
    {


    }

    
    // Update is called once per frame
    public override void OnInspectorGUI() {

        serializedObject.Update();


        DrawNPC(serializedObject.FindProperty("character"), serializedObject);

        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Save To Database", EditorStyles.miniButtonLeft)) 
       // if (GUILayout.Button("Add To Database", EditorStyles.miniButtonLeft)) //
        //if (GUILayout.Button("Load from Database", EditorStyles.miniButtonMid)) //
       // GUILayout.EndHorizontal();


        //DrawDefaultInspector();
    }

    public static void DrawNPC(SerializedProperty character, SerializedObject serializedObject)
    {

        SerializedProperty stats = character.FindPropertyRelative("inspectorStats");
        SerializedProperty needs = character.FindPropertyRelative("inspectorNeeds");
        SerializedProperty wants = character.FindPropertyRelative("inspectorWants");

        SerializedProperty firstName = character.FindPropertyRelative("firstName");
        SerializedProperty middleName = character.FindPropertyRelative("middleName");
        SerializedProperty lastName = character.FindPropertyRelative("lastName");

        firstName.stringValue = EditorGUILayout.TextField("First Name", firstName.stringValue);
        middleName.stringValue = EditorGUILayout.TextField("Middle Name",  middleName.stringValue);
        lastName.stringValue = EditorGUILayout.TextField("Last Name", lastName.stringValue);


        GUILayout.Space(5f);


        if (EditorGUITools.DrawHeader("Stats (" + stats.arraySize + "):", "ShowStats"))
        {
            GUILayout.BeginVertical(EditorStyles.numberField);
            GUILayout.Space(5);


            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            GUILayout.Label("Value:");
            GUILayout.EndHorizontal();

            for (int i = 0, n = stats.arraySize; i < n; i++)
            {
                GUILayout.BeginVertical();
                      

                SerializedProperty stat = stats.GetArrayElementAtIndex(i);
                SerializedProperty statName = stat.FindPropertyRelative("name");
                SerializedProperty statValue = stat.FindPropertyRelative("value");

                // Name, value, delete
                GUILayout.BeginHorizontal();


                statName.stringValue = GUILayout.TextField(statName.stringValue, GUILayout.MinWidth(75));
                statValue.floatValue = EditorGUILayout.FloatField(statValue.floatValue);

                if (GUILayout.Button("X", GUILayout.Width(25))) { stats.DeleteArrayElementAtIndex(i); i = n; }

                GUILayout.EndHorizontal();

        
                GUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Stat", EditorStyles.toolbarButton)) stats.InsertArrayElementAtIndex(stats.arraySize);

            GUILayout.EndVertical();
        }


        #region Needs
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
        #endregion

        #region Wants
        if (EditorGUITools.DrawHeader("Wants (" + wants.arraySize + "):", "ShowWants"))
        {
            GUILayout.BeginVertical(EditorStyles.numberField);
            GUILayout.Space(5);

            for (int i = 0, n = wants.arraySize; i < n; i++)
            {

                // Get the variables from the wants
                SerializedProperty want = wants.GetArrayElementAtIndex(i);
                SerializedProperty wantName = want.FindPropertyRelative("name");


                if (EditorGUITools.DrawHeader(wantName.stringValue, wantName.stringValue + "_menu"))
                {

                    SerializedProperty wantPriority = want.FindPropertyRelative("priority");
                    SerializedProperty priorityModifiers = want.FindPropertyRelative("priorityModifiers");
                    SerializedProperty actions = want.FindPropertyRelative("actions");


                    GUI.color = new Color(0.4f, 0.4f, 0.4f);
                    GUILayout.BeginVertical(EditorStyles.numberField);
                    GUI.color = Color.white;

                    GUILayout.Space(10);

                    // Name, value, priority
                    GUILayout.BeginHorizontal();

                    GUILayout.Label("Name:");
                    wantName.stringValue = GUILayout.TextField(wantName.stringValue, GUILayout.MinWidth(75));


                    GUILayout.Label("Priority: " + wantPriority.intValue);

                    if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(15))) { wants.DeleteArrayElementAtIndex(i); i = n; continue; }

                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);


                    // Show modifiers
                    if (EditorGUITools.DrawHeader("Priority Modifiers (" + priorityModifiers.arraySize + "):", wantName.stringValue + "_priority"))
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
                    if (EditorGUITools.DrawHeader("Actions(" + actions.arraySize + "):", wantName.stringValue + "_actions"))
                    {

                    }


                    GUILayout.EndVertical();

                    if (i != n - 1)
                        GUILayout.Space(10);

                }

            }


            GUILayout.Space(5);
            if (GUILayout.Button("Add Want", EditorStyles.toolbarButton)) wants.InsertArrayElementAtIndex(wants.arraySize);

            GUILayout.EndVertical();
        }
        #endregion


        GUILayout.Space(5f);

        serializedObject.ApplyModifiedProperties();

    }

}

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class NPCEditorWindow : EditorWindow {

    CharacterDatabase characterDatabase;

	string path = "CharacterDatabase.xml";

	[MenuItem("Immersion/NPC Editor")]
	public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(NPCEditorWindow), false, "Character Editor", true);
	}


    void OnEnable()
    {

        characterDatabase = new CharacterDatabase();
        characterDatabase.characters = new List<Character>();
        for (int i = 0, n = characterDatabase.characters.Count; i < n; i++)
        {
            characterDatabase.characters[i] = new Character();
        }

    }

    void OnGUI() {

        if (GUILayout.Button("Load Character")) characterDatabase = CharacterDatabase.Load(path);
        if (GUILayout.Button("Save Character")) { characterDatabase.Save(path); } 
        if (GUILayout.Button("Add Selected Character"))
        {
            if (Selection.activeGameObject.GetComponent<NPC>())
            {
                characterDatabase.AddCharacter(Selection.activeGameObject.GetComponent<NPC>().character);
            }
        }


        if (characterDatabase != null && characterDatabase.characters.Count > 0)
        {
            //	EditorGUILayout.BeginVertical ();
            for (int i = 0, n = characterDatabase.characters.Count; i < n; i++)
            {
                EditorGUILayout.BeginHorizontal();
                characterDatabase.characters[i].firstName = EditorGUILayout.TextField("First Name:", characterDatabase.characters[i].firstName);
                characterDatabase.characters[i].middleName = EditorGUILayout.TextField("Middle Name:", characterDatabase.characters[i].middleName);
                characterDatabase.characters[i].lastName = EditorGUILayout.TextField("Last Name:", characterDatabase.characters[i].lastName);
                EditorGUILayout.EndHorizontal();
            }
            //	EditorGUILayout.EndVertical ();
        }

    }


}

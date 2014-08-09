using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class NPCDataBase : EditorWindow {

    CharacterDatabase characterDatabase;

	string path = "CharacterDatabase.xml";

	[MenuItem("Immersion/NPC Database")]
	public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(NPCDataBase), false, "NPC Database", true);
	}


    void OnEnable()
    {

        characterDatabase = new CharacterDatabase();
        characterDatabase.characters = new List<Character>();
        for (int i = 0, n = characterDatabase.characters.Count; i < n; i++)
        {
            characterDatabase.characters[i] = new Character();
        }

        characterDatabase = CharacterDatabase.Load(path);

    }

    void OnGUI() {

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Characters", EditorStyles.miniButtonLeft)) characterDatabase = CharacterDatabase.Load(path);
        if (GUILayout.Button("Save Characters", EditorStyles.miniButtonMid)) { characterDatabase.Save(path); }
        if (GUILayout.Button("Add New Character", EditorStyles.miniButtonMid)) { characterDatabase.characters.Add(new Character()); }
        if (GUILayout.Button("Generate New Character", EditorStyles.miniButtonMid)) { /* TODO: ADD GENERATION HERE */ } 
        if (GUILayout.Button("Add Selected Character", EditorStyles.miniButtonRight))
        {
            if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<NPC>())
            {
                characterDatabase.AddCharacter(Selection.activeGameObject.GetComponent<NPC>().character);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (characterDatabase != null && characterDatabase.characters.Count > 0)
        {

            MySerializedObject serializedObjectClass = null;

            for (int i = 0, n = characterDatabase.characters.Count; i < n; i++)
            {
                if (EditorGUITools.DrawHeader(characterDatabase.characters[i].firstName + " " + characterDatabase.characters[i].middleName + " " + characterDatabase.characters[i].lastName, characterDatabase.characters[i].firstName + " " + characterDatabase.characters[i].middleName + " " + characterDatabase.characters[i].lastName))
                {
                    GUILayout.BeginVertical(EditorStyles.textArea);

                    serializedObjectClass = (MySerializedObject)ScriptableObject.CreateInstance(typeof(MySerializedObject));
                    serializedObjectClass.value = characterDatabase.characters[i];

                    SerializedObject serilizedNPCClass = new SerializedObject(serializedObjectClass);
                    NPCEditor.DrawNPC(serilizedNPCClass.FindProperty("value"), serilizedNPCClass);

                    if (GUILayout.Button("Remove", EditorStyles.toolbarButton))
                        characterDatabase.characters.RemoveAt(i);
                    
                    EditorGUILayout.EndVertical();
                    
                }
            }
        }

    }



}

public class MySerializedObject : ScriptableObject
{

    public Character value;

    public MySerializedObject(Character value) { this.value = value; }

 
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDebugger : MonoBehaviour {


    public static bool show = false;


    public List<NPC> npcList;
    public List<Rect> windows;



	// Use this for initialization
	void Start () {

        Screen.showCursor = false;
        Screen.lockCursor = true;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.O))
        {
            show = !show;
            Screen.showCursor = show;
            Screen.lockCursor = !show;
        }

        if (show && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                NPC npc = hit.collider.gameObject.GetComponent<NPC>();
                if (npc && !npcList.Contains(npc))
                {
                    npcList.Add(npc);
                    windows.Add(new Rect(0, 0, 100, 100));
                }
            }
        }

	}

    void OnGUI()
    {

        if (show == false)
            return;

        GUILayout.Box("Debbug'n");


        for (int i = 0, n = windows.Count; i < n; i++)
        {

            windows[i] = GUILayout.Window(i, windows[i], DrawNPCWindow, "NPC Debbuger");

        }

    }

    void DrawNPCWindow(int ID)
    {

        NPC npc = npcList[ID];
        Character character = npc.character;


        GUILayout.Box(character.firstName + " " + character.lastName);

        foreach (string key in character.needs.Keys)
        {

            GUILayout.Box(character.needs[key].priority + ". " + key + ": " + character.needs[key].value);

        }

        if (GUILayout.Button("Close"))
        {
            npcList.RemoveAt(ID);
            windows.RemoveAt(ID);
        }
        GUI.DragWindow();

    }

}

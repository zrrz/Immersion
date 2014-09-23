using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDebugger : MonoBehaviour {


    public static bool show = false;


    public List<NPC> npcList;
    public List<Rect> windows;
    public List<Vector2> scrollViews;



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
                    scrollViews.Add(new Vector2(0, 0));
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

//        NPC npc = npcList[ID];
//        Character character = npc.character;
		Character character = null;

        GUILayout.Box(character.firstName + " " + character.lastName, GUILayout.Width(300));

        scrollViews[ID] = GUILayout.BeginScrollView(scrollViews[ID], GUILayout.Height(300));
        GUILayout.BeginVertical();
        int rows = 0;



        GUILayout.Space(10);
        GUILayout.Box("Stats:");
        GUILayout.BeginHorizontal();       
        foreach (string key in character.stats.Keys)
        {                 
            rows++;

            GUILayout.Label(character.stats[key].name + ": " + character.stats[key].value, GUILayout.Width(150));
            if (rows == 2)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                rows = 0;
            }
        }
        rows = 0;
        GUILayout.EndHorizontal();


        GUILayout.Space(10);
        GUILayout.Box("Needs:");
        GUILayout.BeginHorizontal();  
        foreach (string key in character.needs.Keys)
        {
            rows++;
            GUILayout.Label(key + " priority: " + character.needs[key].priority, GUILayout.Width(150));
            if (rows == 2)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                rows = 0;
            }
        }
        rows = 0;
        GUILayout.EndHorizontal();



        GUILayout.Space(10);
        GUILayout.Box("Wants:");
        GUILayout.BeginHorizontal();   
        foreach (string key in character.wants.Keys)
        {
            rows++;
            GUILayout.Label(key + " priority: " + character.wants[key].priority, GUILayout.Width(150));
            if (rows == 2)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                rows = 0;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        
        
        GUILayout.EndVertical();
        if (GUILayout.Button("Close"))
        {
            npcList.RemoveAt(ID);
            windows.RemoveAt(ID);
            scrollViews.RemoveAt(ID);
        }



        GUI.DragWindow();

    }

}

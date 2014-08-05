using UnityEditor;
using UnityEngine;
using System.Collections;

public static class EditorGUITools
{

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key)
    {
        bool state = EditorPrefs.GetBool(key, true);

        //if (!minimalistic) GUILayout.Space(3f);
        if (!state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUI.changed = false;

        text = "<b><size=11>" + text + "</size></b>";
        if (state) text = "\u25BC " + text;
        else text = "\u25BA " + text;
        if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;


        if (GUI.changed) EditorPrefs.SetBool(key, state);

        //if (!minimalistic) GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!state) GUILayout.Space(3f);
        return state;
    }
}
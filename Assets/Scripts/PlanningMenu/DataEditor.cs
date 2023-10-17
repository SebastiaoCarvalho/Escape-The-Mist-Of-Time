using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuManager))]
public class DataEditor : Editor
{
    // This is were we can create our own custom inspector which allows us to instanteate and destroy the map objects
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MenuManager data = (MenuManager)target;

        data.Clean();
    }
   
}

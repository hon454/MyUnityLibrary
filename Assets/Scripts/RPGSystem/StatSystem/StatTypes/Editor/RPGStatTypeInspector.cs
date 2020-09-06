using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RPGStatTypeDatabase))]
public class RPGStatTypeInspector : Editor
{
    private string output = "";
    
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Database that stores all RPGStatTypes.");

        if (GUILayout.Button("Open Editor Window"))
        {
            RPGStatTypeWindow.ShwowWindow();
        }
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RPGStatTypeWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private int _activeId;

    private GUIStyle _toggleButtonStyle;

    private GUIStyle ToggleButtonStyle
    {
        get
        {
            if (_toggleButtonStyle == null)
            {
                _toggleButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
                _toggleButtonStyle.alignment = TextAnchor.MiddleLeft;
            }

            return _toggleButtonStyle;
        }
    }
    
    [MenuItem("Window/RPGSystems/Stat Types")]
    public static void ShwowWindow()
    {
        var window = GetWindow<RPGStatTypeWindow>();
        window.titleContent.text = "Stat Types";
        window.Show();
    }
    
    public void OnEnable()
    {
    }

    public void OnGUI()
    {
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
        
        for (int i = 0; i < RPGStatTypeDatabase.Instance.Count; ++i)
        {
            var asset = RPGStatTypeDatabase.Instance.GetAtIndex(i);
            if (asset != null)
            {
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                GUILayout.Label($"Id: {asset.Id:D3}", GUILayout.Width(60f));

                bool clicked = GUILayout.Toggle(asset.Id == _activeId, asset.Name, ToggleButtonStyle);
                if (clicked != (asset.Id == _activeId))
                {
                    if (clicked)
                    {
                        _activeId = asset.Id;
                        GUI.FocusControl(null);
                    }
                    else
                    {
                        _activeId = -1;
                    }
                }
                
                if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30f)) && EditorUtility.DisplayDialog("Delete Stat Type", "Are you sure you want to delete " + asset.Name + "stat type?", "Delete", "Cancel"))
                {
                    RPGStatTypeDatabase.Instance.RemoveAt(i);
                }
                GUILayout.EndHorizontal();

                if (_activeId == asset.Id)
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.BeginVertical("Box");
                    
                    GUILayout.BeginHorizontal();
                    asset.Icon = EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false, GUILayout.Width(72f), GUILayout.Height(72f)) as Sprite;
                    GUILayout.BeginVertical();
                    
                    GUILayout.Label("Name", GUILayout.Width(80));
                    ++EditorGUI.indentLevel;
                    asset.Name = EditorGUILayout.TextField(asset.Name);
                    --EditorGUI.indentLevel;
                    
                    GUILayout.Label("Name Short", GUILayout.Width(80));
                    ++EditorGUI.indentLevel;
                    asset.NameShort = EditorGUILayout.TextField(asset.NameShort);
                    --EditorGUI.indentLevel;
                    
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Description", GUILayout.Width(80));
                    asset.Description = EditorGUILayout.TextArea(asset.Description);
                    GUILayout.EndHorizontal();
                    
                    EditorGUILayout.EndVertical();
                    
                    if (EditorGUI.EndChangeCheck())
                    {    
                        EditorUtility.SetDirty(RPGStatTypeDatabase.Instance);
                    }
                }
            }
        }
        
        GUILayout.EndScrollView();
        
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Button("Add", EditorStyles.toolbarButton))
        {
            var newAsset = new RPGStatTypeAsset(RPGStatTypeDatabase.Instance.GetNextId());
            RPGStatTypeDatabase.Instance.Add(newAsset);
        }
        
        if (GUILayout.Button("Clear", EditorStyles.toolbarButton) && EditorUtility.DisplayDialog("Clear Stat Types", "Are you sure you want to clear all stat types?", "Clear", "Cancel"))
        {
            RPGStatTypeDatabase.Instance.Clear();
        }

        if (GUILayout.Button("Generate Enum", EditorStyles.toolbarButton))
        {
            RPGStatTypeGenerator.CheckAnGenerateFile();
        }
        
        GUILayout.EndHorizontal();
    }
}


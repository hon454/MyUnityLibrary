using UnityEngine;
using UnityEditor;

public class RPGStatTypeDialog : EditorWindow
{
    private const string windowTitle = "Stat Types";

    public delegate void SelectEvent(RPGStatTypeAsset asset);

    public SelectEvent OnAssetSelect;

    private Vector2 _scroll;

    public static void Display(SelectEvent e)
    {
        var window = GetWindow<RPGStatTypeDialog>(true, windowTitle, true);
        window.OnAssetSelect = e;
        window.Show();
    }

    public void OnGUI()
    {
        _scroll = GUILayout.BeginScrollView(_scroll);
        for (int i = 0; i < RPGStatTypeDatabase.Instance.Count; ++i)
        {
            var asset = RPGStatTypeDatabase.Instance.GetAtIndex(i);
            if (asset != null)
            {
                if (GUILayout.Button(asset.Name, EditorStyles.toolbarButton))
                {
                    OnAssetSelect?.Invoke(asset);
                    Close();
                }
            }
        }
        GUILayout.EndScrollView();
    }
}

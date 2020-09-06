using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SODatabase<T> : AbstractDatabase<T> where T : SODatabaseAsset
{
    protected override void OnAddObject(T obj)
    {
#if  UNITY_EDITOR
        obj.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(obj, this);
        EditorUtility.SetDirty(this);
#endif
    }

    protected override void OnRemoveObject(T obj)
    {
#if  UNITY_EDITOR
        DestroyImmediate(obj, true);
        EditorUtility.SetDirty(this);
#endif
    }
}

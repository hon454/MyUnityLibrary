using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDatabase<T> : AbstractDatabase<T> where T : BaseDatabaseAsset
{
    protected override void OnAddObject(T obj)
    {
#if  UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    protected override void OnRemoveObject(T obj)
    {
#if  UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

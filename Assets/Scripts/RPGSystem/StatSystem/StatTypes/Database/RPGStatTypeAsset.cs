using System;
using UnityEngine;

[Serializable]
public class RPGStatTypeAsset : BaseDatabaseAsset
{
    [SerializeField] private string _nameShort;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    public string NameShort
    {
        get => _nameShort;
        set => _nameShort = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public Sprite Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public RPGStatTypeAsset(int id = -1, string name = "", string nameShort = "", string description = "", Sprite icon = null) : base(id, name)
    {
        NameShort = nameShort;
        Description = description;
        Icon = icon;
    }
}

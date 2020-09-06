using System;
using UnityEngine;

[Serializable]
public class BaseDatabaseAsset : IDatabaseAsset
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public BaseDatabaseAsset(int id = -1, string name = "")
    {
        Id = id;
        Name = name;
    }
}

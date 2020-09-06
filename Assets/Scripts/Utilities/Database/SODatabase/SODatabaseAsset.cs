using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SODatabaseAsset : ScriptableObject, IDatabaseAsset
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

    public SODatabaseAsset(int id = -1, string name = "")
    {
        Id = id;
        Name = name;
    }
}

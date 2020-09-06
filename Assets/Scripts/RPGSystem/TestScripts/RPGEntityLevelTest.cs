using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGEntityLevelTest : MonoBehaviour
{
    [SerializeField] private RPGEntity _entity;
    
    private void Update()
    {
        _entity.EntityLevel.ModifyExp(100);
    }
}

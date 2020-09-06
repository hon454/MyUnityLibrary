using System.Collections.Generic;
using UnityEngine;

public class RPGStatCollection : MonoBehaviour
{
    public Dictionary<RPGStatType, RPGStat> StatDic = new Dictionary<RPGStatType, RPGStat>();
    
    private void Awake()
    {
        ConfigureStats();
    }

    public bool ContainStat(RPGStatType statType)
    {
        return StatDic.ContainsKey(statType);
    }

    public RPGStat GetStat(RPGStatType statType)
    {
        if (ContainStat(statType))
        {
            return StatDic[statType];
        }

        return null;
    }

    public T GetStat<T>(RPGStatType type) where T : RPGStat
    {
        return GetStat(type) as T;
    }

    public T CreatStat<T>(RPGStatType statType) where T : RPGStat
    {
        T stat = System.Activator.CreateInstance<T>();
        StatDic.Add(statType, stat);
        return stat;
    }
    
    public T CreatOrGetStat<T>(RPGStatType statType) where T : RPGStat
    {
        T stat = GetStat<T>(statType);
        if (stat == null)
        {
            stat = CreatStat<T>(statType);
        }
        
        return stat;
    }

    public void AddModifier(RPGStatType target, RPGStatModifier mod, bool update = false)
    {
        if (ContainStat(target))
        {
            var modStat = GetStat(target) as IStatModifiable;
            if (modStat != null)
            {
                modStat.AddModifier(mod);
                if (update == true)
                {
                    modStat.UpdateModifiers();
                }
            }
            else
            {
                Debug.Log("[RPGStats] Trying to add Stat Modifier to non modifiable stat \"" + target.ToString() + "\"");
            }
        }
        else
        {
            Debug.Log("[RPGStats] Trying to add Stat Modifier to \"" + target.ToString() + "\", but RPGStatCollection does no contain that stat");
        }
    }

    public void RemoveStatModifier(RPGStatType target, RPGStatModifier mod, bool update = false)
    {
        if (ContainStat(target))
        {
            var modStat = GetStat(target) as IStatModifiable;
            if (modStat != null)
            {
                modStat.RemoveModifier(mod);
                if (update == true)
                {
                    modStat.UpdateModifiers();
                }
            }
            else
            {
                Debug.Log("[RPGStats] Trying to remove Stat Modifier to non modifiable stat \"" + target.ToString() +
                          "\"");
            }
        }
        else
        {
            Debug.Log("[RPGStats] Trying to remove Stat Modifier to \"" + target.ToString() +
                      "\", but RPGStatCollection does no contain that stat");
        }
    }

    public void ClearAllStatModifier(bool update = false)
    {
        foreach (var target in StatDic.Keys)
        {
            ClearStatModifier(target, update);  
        }
    }

    public void ClearStatModifier(RPGStatType target, bool update = false)
    {
        if (ContainStat(target))
        {
            var modStat = GetStat(target) as IStatModifiable;
            if (modStat != null)
            {
                modStat.ClearModifiers();
                if (update == true)
                {
                    modStat.UpdateModifiers();
                }
            }
            else
            {
                Debug.Log("[RPGStats] Trying to clear Stat Modifier to non modifiable stat \"" + target.ToString() +
                          "\"");
            }
        }
        else
        {
            Debug.Log("[RPGStats] Trying to clear Stat Modifier to \"" + target.ToString() +
                      "\", but RPGStatCollection does no contain that stat");
        }
    }

    public void UpdateAllStatModifier()
    {
        foreach (var target in StatDic.Keys)
        {
            UpdateStatModifier(target);
        }
    }

    public void UpdateStatModifier(RPGStatType target)
    {
        if (ContainStat(target))
        {
            var modStat = GetStat(target) as IStatModifiable;
            if (modStat != null)
            {
                modStat.UpdateModifiers();
            }
            else
            {
                Debug.Log("[RPGStats] Trying to update Stat Modifier to non modifiable stat \"" + target.ToString() +
                          "\"");
            }
        }
        else
        {
            Debug.Log("[RPGStats] Trying to update Stat Modifier to \"" + target.ToString() +
                      "\", but RPGStatCollection does no contain that stat");
        }
    }
    
    protected virtual void ConfigureStats()
    {
    }
}

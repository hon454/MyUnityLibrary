using System;
using UnityEngine;

public class RPGVitalTest : MonoBehaviour
{
    private RPGStatCollection _stats;
    
    private void Start()
    {
        _stats = new RPGDefaultStats();

        var health = _stats.GetStat<RPGVital>(RPGStatType.Health);
        health.OnCurrentValueChange += OnStatValueChange;
        
        DisplayStatValue();

        health.StatCurrentValue -= 75;
        
        DisplayStatValue();
    }

    private void OnStatValueChange(object sender, EventArgs args)
    {
        RPGVital vital = sender as RPGVital;
        if (vital != null)
        {
            Debug.Log($"vital {vital.StatName}'s OnStatValueChange event was triggered");
        }
    }
    
    private void ForEachEnum<T>(Action<T> action)
    {
        if (action != null)
        {
            var statTypes = Enum.GetValues(typeof(T));
            foreach (var statType in statTypes)
            {
                action((T) statType);
            }
        }
    }

    private void DisplayStatValue()
    {
        ForEachEnum<RPGStatType>(statType =>
        {
            RPGStat stat = _stats.GetStat((RPGStatType) statType);
            if (stat != null)
            {
                RPGVital vital = stat as RPGVital;
                if (vital != null)
                {
                    Debug.Log($"Stat {vital.StatName}'s value is {vital.StatCurrentValue}/{vital.StatValue}");
                }
                else
                {
                    Debug.Log($"Stat {stat.StatName}'s value is {stat.StatValue}");
                }
            }
        });
    }
}

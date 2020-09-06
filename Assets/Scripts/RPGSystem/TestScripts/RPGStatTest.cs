using System;
using UnityEngine;

public class RPGStatTest : MonoBehaviour
{
   private RPGStatCollection stats;
   
   private void Start()
   {
      stats = new RPGDefaultStats();
      DisplayStatValue();

      var health = stats.GetStat<RPGStatModifiable>(RPGStatType.Health);
      health.AddModifier(new RPGStatModBasePercent(1.0f, false));
      health.AddModifier(new RPGStatModBaseAdd(50f));
      health.AddModifier(new RPGStatModTotalPercent(1.0f));
      health.UpdateModifiers();

      stats.GetStat<RPGAttribute>(RPGStatType.Stamina).ScaleStat(5);
      stats.GetStat<RPGAttribute>(RPGStatType.Wisdom).ScaleStat(10);
      
      DisplayStatValue();
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
      var statTypes = Enum.GetValues(typeof(RPGStatType));
      foreach (RPGStatType statType in statTypes)
      {
         RPGStat stat = stats.GetStat(statType);
         if (stat != null)
         {
            Debug.Log($"Stat {stat.StatName}'s value is {stat.StatValue}");
         }
      }
   }
}

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RPGStatModifiable : RPGStat, IStatModifiable, IStatValueChange
{
    private List<RPGStatModifier> _statMods;

    public override int StatValue => base.StatValue + StatModifierValue;
    public int StatModifierValue { get; private set; }

    public event EventHandler OnValueChange;
    
    
    public RPGStatModifiable()
    {
        StatModifierValue = 0;
        _statMods = new List<RPGStatModifier>();
    }
    
    public void AddModifier(RPGStatModifier mod)
    {
        _statMods.Add(mod);
        mod.OnValueChange += OnModValueChange;
    }

    public void RemoveModifier(RPGStatModifier mod)
    {
        _statMods.Remove(mod);
        mod.OnValueChange -= OnModValueChange;
    }

    public void ClearModifiers()
    {
        foreach (var mod in _statMods)
        {
            mod.OnValueChange -= OnModValueChange;
        }
        
        _statMods.Clear();
    }

    public void UpdateModifiers()
    {
        StatModifierValue = 0;

        var orderGroups = _statMods.OrderBy(m => m.Order).GroupBy(m => m.Order);
        foreach (var group in orderGroups)
        {
            float sum = 0;
            float max = float.MinValue;
            foreach (var mod in group)
            {
                if (mod.Stacks == false)
                {
                    max = Mathf.Max(max, mod.Value);
                }
                else
                {
                    sum += mod.Value;
                }
            }

            StatModifierValue += group.First().ApplyModifier(
                StatBaseValue + StatModifierValue,
                (sum > max) ? sum : max
            );
        }

        TriggerValueChange();
    }

    public void OnModValueChange(object modifier, EventArgs args)
    {
        UpdateModifiers();
    }
    
    protected void TriggerValueChange()
    {
        OnValueChange?.Invoke(this, null);
    }
}

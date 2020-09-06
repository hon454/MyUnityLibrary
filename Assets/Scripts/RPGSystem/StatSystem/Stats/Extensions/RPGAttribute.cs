using System;
using System.Collections.Generic;
public class RPGAttribute : RPGStatModifiable, IStatScalable, IStatLinkable {
    public int StatLevelValue { get; private set; }
    public int StatLinkerValue => _statLinkerValue;
    public override int StatBaseValue => base.StatBaseValue + StatLevelValue + StatLinkerValue;

    private List<RPGStatLinker> _statLinkers;
    private int _statLinkerValue;
    
    public RPGAttribute()
    {
        _statLinkers = new List<RPGStatLinker>();
    }
    
    public virtual void ScaleStat(int level) {
        StatLevelValue = level;
        TriggerValueChange();
    }

    public void AddLinker(RPGStatLinker linker)
    {
        _statLinkers.Add(linker);
        linker.OnValueChange += OnLinkerValueChange;
    }

    public void ClearLinkers()
    {
        foreach (var linker in _statLinkers)
        {
            linker.OnValueChange -= OnLinkerValueChange;
        }
        _statLinkers.Clear();
    }

    public void UpdateLinkers()
    {
        _statLinkerValue = 0;
        foreach (RPGStatLinker link in _statLinkers)
        {
            _statLinkerValue += link.Value;
        }
        TriggerValueChange();
    }

    private void OnLinkerValueChange(object linker, EventArgs args)
    {
        UpdateLinkers();        
    }
}
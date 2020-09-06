using System;

public abstract class RPGStatLinker : IStatValueChange
{
    public RPGStat Stat { get; private set; }

    public event EventHandler OnValueChange;
    
    public RPGStatLinker(RPGStat stat)
    {
        Stat = stat;

        if (Stat is IStatValueChange iStatValueChange)
        {
            iStatValueChange.OnValueChange += OnLinkedStatValueChange;
        }
        
    }
    
    public abstract int Value { get; }

    private void OnLinkedStatValueChange(object stat, EventArgs args)
    {
        OnValueChange?.Invoke(this, null);
    }

}

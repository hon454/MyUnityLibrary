using System;

public class RPGStat
{
    public string StatName { get; set; }
    public virtual int StatBaseValue { get; set; }
    public virtual int StatValue => StatBaseValue;

    public RPGStat()
    {
        StatName = String.Empty;
        StatBaseValue = 0;
    }

    public RPGStat(string name, int value)
    {
        StatName = name;
        StatBaseValue = value;
    }
}

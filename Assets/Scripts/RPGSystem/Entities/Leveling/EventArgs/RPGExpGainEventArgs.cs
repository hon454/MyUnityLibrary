using System;

public class RPGExpGainEventArgs : EventArgs
{
    public int ExpGained { get; private set; }

    public RPGExpGainEventArgs(int expGained)
    {
        ExpGained = expGained;
    }
}

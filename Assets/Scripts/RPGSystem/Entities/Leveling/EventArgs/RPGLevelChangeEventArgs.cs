using System;

public class RPGLevelChangeEventArgs : EventArgs
{
    public int NewLevel { get; private set; }
    public int OldLevel { get; private set; }

    public RPGLevelChangeEventArgs(int newLevel, int oldLevel)
    {
        NewLevel = newLevel;
        OldLevel = oldLevel;
    }
}

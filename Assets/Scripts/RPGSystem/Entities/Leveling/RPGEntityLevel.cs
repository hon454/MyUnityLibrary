using System;
using UnityEngine;

public abstract class  RPGEntityLevel : MonoBehaviour
{
    [SerializeField] private int _level = 0;
    [SerializeField] private int _levelMin = 0;
    [SerializeField] private int _levelMax = 100;

    public event EventHandler<RPGExpGainEventArgs> OnEntityExpGain;
    public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelChange;
    public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelUp;
    public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelDown;
    
    public int Level
    {
        get => _level;
        set => _level = value;
    }

    public int LevelMin
    {
        get => _levelMin;
        set => _levelMin = value;
    }

    public int LevelMax
    {
        get => _levelMax;
        set => _levelMax = value;
    }

    public int ExpCurrent { get; set; }

    public int ExpRequired { get; set; }


    private void Awake()
    {
        ExpRequired = GetExpRequiredForLevel(Level);
    }

    public abstract int GetExpRequiredForLevel(int level);
    
    public void ModifyExp(int amount)
    {
        ExpCurrent += amount;
        OnEntityExpGain?.Invoke(this, new RPGExpGainEventArgs(amount));
        CheckCurrentExp();
    }

    public void SetCurrentExp(int value)
    {
        int expGained = value - ExpCurrent;

        ExpCurrent = value;
        
        OnEntityExpGain?.Invoke(this, new RPGExpGainEventArgs(expGained));
        
        CheckCurrentExp();
    }

    public void CheckCurrentExp()
    {
        int oldLevel = Level;
        
        InternalCheckCurrentExp();
        
        if (oldLevel != Level)
        {
            OnEntityLevelUp?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }
    
    private void InternalCheckCurrentExp()
    {
        while (true)
        {
            if (ExpCurrent > ExpRequired)
            {
                ExpCurrent -= ExpRequired;
                InternalIncreaseCurrentLevel();
            }
            else if (ExpCurrent < 0)
            {
                ExpCurrent += GetExpRequiredForLevel(Level - 1);
                InternalDecreaseCurrentLevel();
            }
            else
            {
                break;
            }
        }
    }

    public void IncreaseCurrentLevel()
    {
        int oldLevel = Level;

        InternalIncreaseCurrentLevel();
        
        if (oldLevel != Level)
        {
            OnEntityLevelChange?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }

    private void InternalIncreaseCurrentLevel()
    {
        int oldLevel = Level++;

        if (Level > LevelMax)
        {
            Level = LevelMax;
            ExpCurrent = GetExpRequiredForLevel(Level);
        }

        ExpRequired = GetExpRequiredForLevel(Level);
        
        if (oldLevel != Level)
        {
            OnEntityLevelUp?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }

    public void DecreaseCurrentLevel()
    {
        int oldLevel = Level;

        InternalDecreaseCurrentLevel();
        
        if (oldLevel != Level)
        {
            OnEntityLevelChange?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }

    private void InternalDecreaseCurrentLevel()
    {
        int oldLevel = Level--;

        if (Level < LevelMin)
        {
            Level = LevelMin;
            ExpCurrent = 0;
        }

        ExpRequired = GetExpRequiredForLevel(Level);
        
        if (oldLevel != Level)
        {
            OnEntityLevelDown?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }

    public void SetLevel(int targetLevel, bool clearExp = true)
    {
        int oldLevel = Level;
        
        Level = targetLevel;
        ExpRequired = GetExpRequiredForLevel(Level);

        if (clearExp)
        {
            SetCurrentExp(0);
        }
        else
        {
            InternalCheckCurrentExp();
        }

        if (oldLevel != Level)
        {
            OnEntityLevelChange?.Invoke(this, new RPGLevelChangeEventArgs(Level, oldLevel));
        }
    }
}

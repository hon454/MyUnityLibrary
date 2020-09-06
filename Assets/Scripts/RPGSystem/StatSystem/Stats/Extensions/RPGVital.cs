﻿using System;

public class RPGVital : RPGAttribute, IStatCurrentValueChange
{
    private int _statCurrentValue;
    public event EventHandler OnCurrentValueChange;
    
    public int StatCurrentValue
    {
        get
        {
            if (_statCurrentValue > StatValue)
            {
                _statCurrentValue = StatValue;
            } 
            else if (_statCurrentValue < 0)
            {
                _statCurrentValue = 0;
            }
            return _statCurrentValue;
        }
        set
        {
            if (_statCurrentValue != value)
            {
                _statCurrentValue = value;
                OnCurrentValueChange?.Invoke(this, null);
            }
        }
    }

    public RPGVital()
    {
        _statCurrentValue = 0;
    }

    public void SetCurrentValueToMax()
    {
        StatCurrentValue = StatValue;
    }
}

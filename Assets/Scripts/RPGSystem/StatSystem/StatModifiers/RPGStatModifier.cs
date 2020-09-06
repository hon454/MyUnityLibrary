using System;

public abstract class RPGStatModifier
{
    private float _value;
    
    public event EventHandler OnValueChange;

    public abstract int Order { get; }

    public bool Stacks { get; set; }
    
    public float Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value))
                return;
            
            _value = value;
            OnValueChange?.Invoke(this, null);
        }
    }

    public RPGStatModifier(float value)
    {
        _value = value;
        Stacks = false;
    }

    public RPGStatModifier(float value, bool stacks)
    {
        _value = value;
        Stacks = stacks;
    }

    public abstract int ApplyModifier(int statValue, float modValue);
}

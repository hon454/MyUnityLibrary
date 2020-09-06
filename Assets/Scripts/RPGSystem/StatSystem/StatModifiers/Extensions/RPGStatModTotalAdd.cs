using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGStatModTotalAdd : RPGStatModifier
{
    public override int Order => 4;
    
    public RPGStatModTotalAdd(float value) : base(value)
    {
    }

    public RPGStatModTotalAdd(float value, bool stacks) : base(value, stacks)
    {
    }
    
    public override int ApplyModifier(int statValue, float modValue)
    {
        return (int) (modValue);
    }
}

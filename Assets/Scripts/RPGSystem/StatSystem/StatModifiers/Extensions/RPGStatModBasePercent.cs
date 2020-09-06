
public class RPGStatModBasePercent : RPGStatModifier
{
    public override int Order => 1;
    
    public RPGStatModBasePercent(float value) : base(value)
    {
    }

    public RPGStatModBasePercent(float value, bool stacks) : base(value, stacks)
    {
    }
    
    public override int ApplyModifier(int statValue, float modValue)
    {
        return (int) (statValue * modValue);
    }
}

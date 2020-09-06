public class RPGStatModTotalPercent : RPGStatModifier
{
    public override int Order => 3;
    
    public RPGStatModTotalPercent(float value) : base(value)
    {
    }

    public RPGStatModTotalPercent(float value, bool stacks) : base(value, stacks)
    {
    }
    
    public override int ApplyModifier(int statValue, float modValue)
    {
        return (int) (statValue * modValue);
    }
}
public class RPGStatModBaseAdd : RPGStatModifier
{
    public override int Order => 2;
    
    public RPGStatModBaseAdd(float value) : base(value)
    {
    }

    public RPGStatModBaseAdd(float value, bool stacks) : base(value, stacks)
    {
    }
    
    public override int ApplyModifier(int statValue, float modValue)
    {
        return (int) (modValue);
    }
}

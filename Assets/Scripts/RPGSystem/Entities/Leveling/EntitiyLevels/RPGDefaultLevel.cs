using UnityEngine;

public class RPGDefaultLevel : RPGEntityLevel
{
    public override int GetExpRequiredForLevel(int level)
    {
        return (int) (Mathf.Pow(level, 2f) * 100f) + 100;
    }
}

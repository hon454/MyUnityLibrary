public class RPGDefaultStats : RPGStatCollection
{
    protected override void ConfigureStats()
    {
        base.ConfigureStats();
        var stamina = CreatOrGetStat<RPGAttribute>(RPGStatType.Stamina);
        stamina.StatName = "Stamina";
        stamina.StatBaseValue = 10;
        
        var wisdom = CreatOrGetStat<RPGAttribute>(RPGStatType.Wisdom);
        wisdom.StatName = "Wisdom";
        wisdom.StatBaseValue = 5;
        
        var health = CreatOrGetStat<RPGVital>(RPGStatType.Health);
        health.StatName = "Health";
        health.StatBaseValue = 100;
        // health.AddLinker(new RPGStatLinkerBasic(CreatOrGetStat<RPGAttribute>(RPGStatType.Stamina), 10f));
        // health.UpdateLinkers();
        health.SetCurrentValueToMax();
        
        var mana = CreatOrGetStat<RPGVital>(RPGStatType.Mana);
        mana.StatName = "Mana";
        mana.StatBaseValue = 2000;
        mana.AddLinker(new RPGStatLinkerBasic(CreatOrGetStat<RPGAttribute>(RPGStatType.Wisdom), 50f));
        mana.UpdateLinkers();
        mana.SetCurrentValueToMax();
    }
}

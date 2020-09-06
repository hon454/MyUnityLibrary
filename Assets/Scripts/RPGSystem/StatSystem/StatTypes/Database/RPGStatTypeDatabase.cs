public class RPGStatTypeDatabase : BaseDatabase<RPGStatTypeAsset>
{
    private const string databasePath = @"Resources/RPGSystems/Databases/";
    private const string databseName = @"StatTypeDatabase.asset";

    private static RPGStatTypeDatabase _instance = null;

    public static RPGStatTypeDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GetDataBase<RPGStatTypeDatabase>(databasePath, databseName);
            }

            return _instance;
        }        
    }
}

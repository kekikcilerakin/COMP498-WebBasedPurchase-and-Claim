public static class DBManager
{
    public static string username;
    public static int gold;
    public static int level;
    public static int damage;
    public static int critChance;
    public static int autoClickDamage;

    public static bool LoggedIn { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }
}

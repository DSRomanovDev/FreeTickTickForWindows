public static class UserDao
{
    private static AppConfig _config;

    public static void Initialize(AppConfig config)
    {
        _config = config;
    }

    public static bool IsPro()
    {
        return _config.ForceProStatus;
    }
}

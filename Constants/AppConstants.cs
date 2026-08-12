namespace AppPickingMinhasCompras.Constants;

public static class AppConstants
{
    public const string DatabaseFileName = "apppicking.db";
    
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    
    public const string AppTitle = "App Picking - Minhas Compras";
    public const string AppVersion = "1.0.0";
}

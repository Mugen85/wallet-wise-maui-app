namespace WalletWise.Services;

public static class FileLogger
{
    private static string LogPath => Path.Combine(FileSystem.AppDataDirectory, "walletwise-log.txt");

    public static void Log(string message)
    {
        try
        {
            var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}{Environment.NewLine}";
            File.AppendAllText(LogPath, line);
        }
        catch { /* silent */ }
    }

    public static string ReadLog()
    {
        try { return File.Exists(LogPath) ? File.ReadAllText(LogPath) : "Log vuoto."; }
        catch { return "Errore lettura log."; }
    }

    public static void Clear()
    {
        try { if (File.Exists(LogPath)) File.Delete(LogPath); }
        catch { /* silent */ }
    }
}
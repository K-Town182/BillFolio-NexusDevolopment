using System.IO;
using Microsoft.Maui.Storage;

public static class Constants
{
    public const string DatabaseFilename = "TodoSQLite.db3";
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}

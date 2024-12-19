using ConsoleAppCSVFileToDatabase.Constants;

namespace ConsoleAppCSVFileToDatabase.Helpers;

public class FileHelper
{
    public static string GetFilePath(string fileName)
    {
        return Path.GetFullPath($"{FileConstants.AssetsFolder}\\{fileName}.csv");
    }
    
    public static string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }
    
    public static string ReadFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            return reader.ReadToEnd();
        }
    }
}
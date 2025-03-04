namespace ConsoleAppCSVFileToDatabase;

public static class Config
{
    public static string DbName = "CSVDb";
    public static string DbUrl = "https://a.free.dotnet.ravendb.cloud/";
    public static string AssetsFolder = Path.GetFullPath("Assets");
    public static string DbCerificateSubject = "CN=free.dotnet";
}
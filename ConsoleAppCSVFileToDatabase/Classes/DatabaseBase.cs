using System.Security.Cryptography.X509Certificates;
using ConsoleAppCSVFileToDatabase.Helpers;
using Raven.Client.Documents;

namespace ConsoleAppCSVFileToDatabase;

public class DatabaseBase
{
    private DocumentStore DocumentStore;
    protected DatabaseBase(string dbName, X509Certificate2 certificate, string dbUrl)
    {
        DocumentStore = new DocumentStore
        {
            Urls = [dbUrl],
            Database = dbName,
            Certificate = certificate
        };
        DocumentStore.Initialize();
    }
}
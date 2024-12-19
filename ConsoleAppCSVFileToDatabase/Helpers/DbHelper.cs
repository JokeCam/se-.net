using ConsoleAppCSVFileToDatabase.Constants;
using ConsoleAppCSVFileToDatabase.Entities;
using Raven.Client.Documents;

namespace ConsoleAppCSVFileToDatabase.Helpers;

public static class DbHelper
{
    static DbHelper()
    {
        DocumentStore = new DocumentStore
       {
           Urls = new[] // URL to the Server,
           {
               // or list of URLs 
               DbConstants.DbUrl
           },
           Database = DbConstants.DbName,
           Conventions = { }, // DocumentStore customizations,
           Certificate = CertificateHelper.Certificate
       };
        DocumentStore.Initialize(); // Each DocumentStore needs to be initialized before use.
        // This process establishes the connection with the Server
        // and downloads various configurations
        // e.g. cluster topology or client configuration
    }
    
    public static DocumentStore DocumentStore { get; set; }
}
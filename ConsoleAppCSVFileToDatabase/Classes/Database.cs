using System.Security.Cryptography.X509Certificates;
using ConsoleAppCSVFileToDatabase.Entities;
using Raven.Client.Documents;

namespace ConsoleAppCSVFileToDatabase;

public class Database : DatabaseBase
{
    public Database(string dbName, X509Certificate2 certificate, string dbUrl) : base(dbName, certificate, dbUrl) { }

    public async Task<int> SaveFileRecord(ICsvRecord[] records)
    {
        return await Task.Run<int>(() =>
        {
            var store = new DocumentStore();
            var session = store.OpenSession();
            session.Store(records);
            session.SaveChanges();
            return 1;
        });
    }
}
using System.Security.Cryptography.X509Certificates;
using ConsoleAppCSVFileToDatabase;
using ConsoleAppCSVFileToDatabase.Entities;
using ConsoleAppCSVFileToDatabase.Helpers;

Console.WriteLine("Please enter cvs file name from the Assets folder (no extension):");
var fileName = Console.ReadLine();

X509Certificate2 certificate = await new Certificate(Config.DbCerificateSubject).GetCertificate();
var fileEnumerator = new CsvEnumerable<ICsvRecord>(fileName).GetEnumerator();

ICsvRecord[] fileRecords = [];
while (fileEnumerator.MoveNext())
{
    fileRecords.Append(fileEnumerator.Current);
}

var Db = new Database(Config.DbName, certificate, Config.DbUrl);
await Db.SaveFileRecord(fileRecords);

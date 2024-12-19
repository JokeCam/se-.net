// See https://aka.ms/new-console-template for more information

using ConsoleAppCSVFileToDatabase.Entities;
using ConsoleAppCSVFileToDatabase.Helpers;

Console.WriteLine("Please enter cvs file name from the Assets folder (no extension):");
var fileName = Console.ReadLine();
WriteFileToDb(fileName);

void WriteFileToDb(string fileName)
{
    if (fileName == null)
    {
        Console.WriteLine("File name is null!");
        return;
    }
    
    var store = DbHelper.DocumentStore;
    var filePath = FileHelper.GetFilePath(fileName);
    var file = new CsvFile
    {
        Name = FileHelper.GetFileName(filePath),
        Contents = FileHelper.ReadFile(filePath)
    };

    try
    {
        var session = store.OpenSession();
        session.Store(file);
        session.SaveChanges();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    finally
    {
        Console.WriteLine("Saved successfully!");
    }
}
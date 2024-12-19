using ConsoleAppCSVFileToDatabase.Interfaces;

namespace ConsoleAppCSVFileToDatabase.Entities;

public class CsvFile : ICsvFile
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Contents { get; set; }
}
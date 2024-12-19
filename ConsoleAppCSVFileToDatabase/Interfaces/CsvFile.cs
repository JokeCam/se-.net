namespace ConsoleAppCSVFileToDatabase.Interfaces;

public interface ICsvFile
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Contents { get; set; }
}


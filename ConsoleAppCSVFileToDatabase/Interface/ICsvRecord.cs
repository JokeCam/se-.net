namespace ConsoleAppCSVFileToDatabase.Entities;

public interface ICsvRecord
{
    public string Id { get; set; }
    public string Name { get; set; }
}
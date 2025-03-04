using System.Collections;
using System.Globalization;
using CsvHelper;

namespace ConsoleAppCSVFileToDatabase;

public class CsvEnumerable<T> : IEnumerable<T>
{
    private string fileName;
    public CsvEnumerable(string fileName)
    {
        this.fileName = fileName;
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        using (var reader = new StreamReader($"{Config.AssetsFolder}/{fileName}"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<T>().GetEnumerator();
        }
    }
}
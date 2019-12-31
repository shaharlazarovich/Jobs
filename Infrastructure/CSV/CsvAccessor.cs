using System.IO;
using Application.Interfaces;
using CsvHelper;
using Domain;

namespace Infrastructure.Photos
{
    public class CsvAccessor : ICsvAccessor
    {
        public CsvAccessor(){
        }

        public System.Collections.Generic.IEnumerable<Job> AddCsv(string Path)
        {
            TextReader reader = new StreamReader("import.txt");
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<Job>();
            return records;
        }
        public string DeleteCsv(string Path)
        {
            return "";
        }
    }
}
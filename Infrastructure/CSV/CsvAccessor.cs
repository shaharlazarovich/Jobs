using Application.Interfaces;
using CsvHelper;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.CSV
{
    public class CsvAccessor : ICsvAccessor
        {
            public List<Job> ReadCsvFileToJobModel(string path)
            {
                try
                {
                    using (var reader = new StreamReader(path, Encoding.Default))
                    using (var csv = new CsvReader(reader))
                    {
                        var records = csv.GetRecords<Job>().ToList();
                        return records;
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    throw new Exception(e.Message);
                }
                catch (FieldValidationException e)
                {
                    throw new Exception(e.Message);
                }
                catch (CsvHelperException e)
                {
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

        public void WriteNewCsvFile(string path, List<Job> jobModels)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                cw.WriteHeader<Job>();
                cw.NextRecord();
                foreach (Job emp in jobModels)
                {
                    cw.WriteRecord<Job>(emp);
                    cw.NextRecord();
                }
            }
        }
    }
}
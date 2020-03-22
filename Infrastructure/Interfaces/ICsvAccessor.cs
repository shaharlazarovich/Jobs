using Domain;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface ICsvAccessor
    {
        List<Job> ReadCsvFileToJobModel(string path);
        void WriteNewCsvFile(string path, List<Job> jobModels);
    }
}
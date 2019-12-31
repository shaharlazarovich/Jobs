using Domain;

namespace Application.Interfaces
{
    public interface ICsvAccessor
    {
         System.Collections.Generic.IEnumerable<Job> AddCsv(string Path);
         string DeleteCsv(string Path);
    }
}
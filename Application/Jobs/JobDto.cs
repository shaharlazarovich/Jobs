using System;
using static Application.Jobs.Enums;

namespace Application.Jobs
{
    public class JobDto
    {
        public long Id { get; set; }
        public string JobName { get; set; }
        public string Company { get; set; }
        public string Replication { get; set; }
        public string Servers { get; set; }
        public DateTime LastRun { get; set; }
        public string RTA { get; set; }
        public string Results { get; set; }
        public string Key { get; set; }
        public string RTONeeded { get; set; }
        public string JobIP { get; set; }
        public JobStatus JobStatus { get; set; }

    }
}
using System;

namespace Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string JobName { get; set; }
        public string Company { get; set; }
        public string Replication { get; set; }
        public string Servers { get; set; }
        public DateTime LastRun { get; set; }
        public int RTA { get; set; }
        public string Results { get; set; }
        public string Key { get; set; }
        public int RTONeeded { get; set; }
    }
}
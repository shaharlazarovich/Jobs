using System;

namespace Application.Jobs
{
    public class JobDto
    {
        public Guid Id { get; set; }
        public string JobName { get; set; }
        public string Company { get; set; }
        public string Replication { get; set; }
        public string Servers { get; set; }

    }
}
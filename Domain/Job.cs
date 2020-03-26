using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Enums;

namespace Domain
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual List<JobSummaryResult> JobSummaryResults { get; set; }
        public virtual List<Device> Devices { get; set; }
        public JobStatus jobStatus { get; set; } // Active / Disabled
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class JobSummaryResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string DataMover { get; set; }
        public string RealDowntime { get; set; }
        public string JobStatus { get; set; }
        public long ServersInStatusOK { get; set; } 
        public long ServersInStatusBAD { get; set; } 
        public DateTime RunDate { get; set; }
        public DateTime NextRun { get; set; }
        public bool PreTestTask { get; set; }
        public bool FailOver { get; set; }
        public bool ServerTest { get; set; }
        public bool SnapShotVMs { get; set; }
        public bool CleanupFailOver { get; set; }
        public string GlobalSiteMap { get; set; } //string of json
        public long NumberOfVMs { get; set; }
        public long NumberOfPhisicals { get; set; }
        public long NumberOfDevices { get; set; }
        public decimal RTA { get; set; } //recovery time actual
        public decimal RTO { get; set; } //Recover Time Objective
        public int Score { get; set; }
        public int PreviousScore { get; set; }
        public decimal iopsCapacity { get; set; }
        public decimal iopsActual { get; set; }
        public decimal ramCapacity { get; set; }
        public decimal ramActual { get; set; }
        public decimal cpuCapacity { get; set; }
        public decimal cpuActual { get; set; }
        public string KnownIssues { get; set; } //string of json
        public virtual List<JobDeviceResult> Servers { get; set; }
        public string SummaryOverTime { get; set; } //string of json
    }

    //further is utility domain classes that will not go into db schema, 
    //but will be generated every time  
    public class KnownIssue
    {
        public string IssueName { get; set; }
        public long NumberOfOccurrence  { get; set; }
    }

    public class Gif
    {
        public string machineName { get; set; }
        public string testName  { get; set; }
        public string groupName { get; set; }
        public int[] iconsFlags  { get; set; }
        public string GifUrl  { get; set; }
    }

    public class SummaryOverTime
    {
        public string quarterName { get; set; }
        public string greenSrvers  { get; set; }
        public string yellowServers { get; set; }
        public string redServers { get; set; }
    }

    public class JobSummaryResultParsed
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string DataMover { get; set; }
        public string RealDowntime { get; set; }
        public string JobStatus { get; set; }
        public long ServersInStatusOK { get; set; } 
        public long ServersInStatusBAD { get; set; } 
        public DateTime RunDate { get; set; }
        public DateTime NextRun { get; set; }
        public bool PreTestTask { get; set; }
        public bool FailOver { get; set; }
        public bool ServerTest { get; set; }
        public bool SnapShotVMs { get; set; }
        public bool CleanupFailOver { get; set; }

        //public string GlobalSiteMap { get; set; } 
        public long NumberOfVMs { get; set; }
        public long NumberOfPhisicals { get; set; }
        public long NumberOfDevices { get; set; }
        public decimal RTA { get; set; } //recovery time actual
        public decimal RTO { get; set; } //Recover Time Objective
        public int Score { get; set; }
        public decimal iopsCapacity { get; set; }
        public decimal iopsActual { get; set; }
        public decimal ramCapacity { get; set; }
        public decimal ramActual { get; set; }
        public decimal cpuCapacity { get; set; }
        public decimal cpuActual { get; set; }
        public List<KnownIssue> KnownIssues { get; set; } 
        public virtual List<JobDeviceResult> Servers { get; set; }
        public List<Gif> Gifs { get; set; } 
        public List<SummaryOverTime> SummaryOverTime { get; set; } 
    }
}

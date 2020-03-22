using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class IncomingJobSummaryResult //not in use, leave it for use latter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string blob { get; set; }
    }

    public class IncomingJobSummaryResultOlD //not in use, leave it for use latter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string dataMover { get; set; }
        public string score { get; set; }
        public string realDowntime { get; set; }
        public string jobStatus { get; set; }
        public string runDate { get; set; }
        public string serversStatus { get; set; }
        public string nextRun { get; set; }
        public string lastMonthData { get; set; }
        public string timeToRecover { get; set; }
        public string preTestTask { get; set; }
        public string failOver { get; set; }
        public string serverTest { get; set; }
        public string snapshotVMs { get; set; }
        public string cleanupFailOver { get; set; }
        public string globalSiteMap { get; set; }
        public string NumberOfVMs { get; set; }
        public string NumberOfPhisicals { get; set; }
        public string networkDevices { get; set; }
        public string iopsCapacity { get; set; }
        public string iopsUsed { get; set; }
        public string cpuCapacity { get; set; }
        public string cpuUsed { get; set; }
        public string ramCapacity { get; set; }
        public string ramUsed { get; set; }
        public string knownIssues { get; set; }
        public string powerOn { get; set; }
        public string service { get; set; }
        public string network { get; set; }
        public string database { get; set; }
        public string webPortal { get; set; }
        public string servers { get; set; }
        public string gifs { get; set; }
        public string summaryOverTime { get; set; }
    }
}
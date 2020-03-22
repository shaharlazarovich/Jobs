using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class JobDeviceResult
    {   public long Id { get; set; }
        public long JobId { get; set; }
        public long JobSummaryResultId { get; set; }
        public string machineName { get; set; }
        public string Status { get; set; }

        public int type { get; set; } //plan / custom /physical etc.

        public string results { get; set; }

        public int testGroup { get; set; }

        public string TargetServer { get; set; }

        public string JobHealthBeforeTest { get; set; }

        public string HostServer { get; set; }

        public string HostServerType { get; set; }

        public string UndoTestFailOverStatus { get; set; }

        public string AutoTroubleshooting { get; set; }

       public string smallGif {get; set;}
       public string largeGif {get; set;}

        public virtual List<JobTestResult> JobTestResults { get; set; }
    }
}
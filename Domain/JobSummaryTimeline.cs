using System;
using System.Collections.Generic;

namespace Domain
{
    public class JobSummaryTimelineRecord {
        public long Id {get; set;}
        public DateTime RunDate {get; set;}
    }
    public class JobSummaryTimeline {
        public DateTime date {get; set;}
        public string jobName {get; set;}

        public int score {get; set;}
        public List<JobSummaryTimelineRecord> results {get; set;} 
    }
}
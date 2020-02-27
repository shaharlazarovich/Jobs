using System;

namespace Domain
{
    public class JobAction
    {
        public Guid Id { get; set; }
        public string JobId { get; set; }
        public string JobName { get; set; }
        public string UserId { get; set; }
        public DateTime ActionDate { get; set; }
        public string RemoteIP { get; set; }
        public string RemoteResponse { get; set; }
        public string RequestProperties { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        
    }

}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class JobAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
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
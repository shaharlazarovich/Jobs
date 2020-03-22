using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Enums;

namespace Domain
{
    public class JobTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
        public long JobDeviceResultId { get; set; }
        public long testType { get; set; }     // poweron /application / service /network/ database/ custom and so on
        public string testResult { get; set; }
        public string mediaURL { get; set; } //currently for web
        public TestStatus TestStatus { get; set; }
        [NotMapped]
        public virtual string name  {get; set;}
    }
}
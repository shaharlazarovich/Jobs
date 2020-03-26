using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {
    public class DRTest {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public long DeviceId {get; set;}
    }
}
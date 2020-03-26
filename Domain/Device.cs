using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string DeviceIP { get; set; }
        public string DeviceUser { get; set; }
        public string DevicePassword { get; set; }
        public string DeviceGroup { get; set; }
        public virtual List<DRTest> DRTests { get; set; }
    }
}

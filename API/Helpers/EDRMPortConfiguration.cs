using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class EDRMPortConfiguration
    {
        public int ServicePort { get; set; }
        public int WorkerProcessPort { get; set; }
        public bool ListenOnlyOnLocalhost { get; set; }
    }
}

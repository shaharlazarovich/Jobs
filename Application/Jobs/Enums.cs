using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Jobs
{
    public class Enums
    {
        public enum JobStatus
        {
            Active = 0,
            Deleted = 1,
            Disabled = 2
        }
        public enum TestStatus
        {
            Success = 0,
            Failed = 1,
            NA = 2
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Job : Base
    {
        public Guid JobID { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public Guid ProjectID { get; set; }
        public JobStatus JobStatus { get; set; }
        public JobTag JobTag { get; set; }
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set;}
        public string Description { get; set; }
        public Guid ParentID { get; set; }
    }
}

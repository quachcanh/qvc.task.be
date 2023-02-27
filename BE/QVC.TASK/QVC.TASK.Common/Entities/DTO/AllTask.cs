using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common.Entities.DTO
{
    public class AllTask : Base
    {
        public Guid AssignID { get; set; }
        public Guid EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid JobID { get; set; }
        public string? Description { get; set; }

        public string JobCode { get; set; }
        public string JobName { get; set; }
        public Guid ProjectID { get; set; }
        public JobStatus JobStatus { get; set; }
        public JobTag? JobTag { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? ParentID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Assign : Base
    {
        public Guid AssignID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid JobID { get; set; }
        public string? Description { get; set; }
        public Assign()
        {
            AssignID= Guid.NewGuid();
        }
    }
}

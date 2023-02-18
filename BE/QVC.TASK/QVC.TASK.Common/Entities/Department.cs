using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Department : Base
    {
        public Guid DepartmentID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set;}
        public Guid CompanyID { get; set; }
        public Guid ParentID { get; set;}
        public Department()
        {
            DepartmentID = Guid.NewGuid();
        }
    }
}

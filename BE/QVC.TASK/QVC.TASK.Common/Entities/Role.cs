using QVC.TASK.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Role : Base
    {
        public Guid RoleID { get; set; }
        public Enums.Role Access { get; set; }
        public string Description { get; set; }
    }
}

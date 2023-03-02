using QVC.TASK.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common.Entities.DTO
{
    public class GetAllInput
    {
        public string DBDomain { get; set; }
        public string DBCompany { get; set; }
        public State State { get; set; }
    }
    
    public class MyTaskInput
    {
        public Guid Id { get; set; }
        public string DBDomain { get; set; }
        public string DBCompany { get; set; }
        public State State { get; set; }
        public JobStatus Type { get; set; }
    }
}

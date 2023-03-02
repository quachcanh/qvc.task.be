using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public enum JobStatus
    {
        NeedToDo = 0,
        Processing = 1,
        Complete = 2,
        OutOfDate = 3,
        Assignment = 4
    }
}

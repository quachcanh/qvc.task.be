using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class JobReports
    {
        public int Complete { get; set; }
        public int Processing { get; set; }
        public int Todo { get; set; }
        public int OutOfDate { get; set; }
    }
}

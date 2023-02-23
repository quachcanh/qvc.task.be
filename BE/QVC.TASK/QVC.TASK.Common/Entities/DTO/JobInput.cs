using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class JobInput
    {
        public DataInsert<Job> DataInsert { get; set; }
        public List<Job> DataList { get; set;}
    }
}

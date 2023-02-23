using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class DataInsert<T>
    {
        public T Data { get; set; }
        public string DBDomain { get; set; }
    }

    public class DataGetJob
    {
        public List<Guid> Id { get; set; }
        public string DBDomain { get; set; }
    }
}

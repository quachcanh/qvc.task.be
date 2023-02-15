using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Position : Base
    {
        public Guid PositionID { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
    }
}

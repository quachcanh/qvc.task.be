using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Company : Base
    {
        public Guid CompanyID { get; set; }
        public string CompanyCode { get; set;}
        public string CompanyName { get; set;}
        public string UserName { get; set;}
        public Company()
        {
            CompanyID= Guid.NewGuid();
        }
    }
}

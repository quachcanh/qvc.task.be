using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public interface IAssignDL : IBaseDL<Assign>
    {
        public List<AllTask> GetAllMyTask(Guid id, string db);
    }
}

using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class AssignBL : BaseBL<Assign>, IAssignBL
    {
        #region Field

        private IAssignDL _assignDL;

        #endregion

        #region Constructor

        public AssignBL(IAssignDL assignDL) : base(assignDL)
        {
            _assignDL = assignDL;


            #endregion
        }

        public List<AllTask> GetAllMyTask(Guid id, string dbdomain, string dbcompany)
        {
            List<AllTask> allTasks= new List<AllTask>();
            List<AllTask> task2 = new List<AllTask>();
            var task1 = _assignDL.GetAllMyTask(id, dbdomain);
            if(dbcompany!= null && dbcompany!= string.Empty && dbcompany!="null")
            {
                task2 = _assignDL.GetAllMyTask(id, dbcompany);
            }
            
            if(task1?.Count>0|| task2?.Count>0)
            {
                allTasks = task1.Concat(task2).ToList();
                
            }
            return allTasks.Distinct().ToList();
        }
    }
}

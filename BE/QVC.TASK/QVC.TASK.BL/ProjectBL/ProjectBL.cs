using Mysqlx.Resultset;
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
    public class ProjectBL : BaseBL<Project>, IProjectBL
    {
        #region Field

        private IProjectDL _projectDL;

        #endregion
        #region Constructor

        public ProjectBL(IProjectDL projectDL) : base(projectDL)
        {
            _projectDL = projectDL;
        }

        #endregion

        public List<Project> GetAllProject(GetAllInput input)
        {
            List<Project> result = new List<Project>();
            if (input != null)
            {
                // Là cá nhân
                if (input.State == Common.Enums.State.CaNhan)
                {
                    result = _projectDL.GetAll(input.DBDomain);
                }
                else
                {
                    // Là công ty
                    if (input.DBDomain == input.DBCompany)
                    {
                        // Là chính chỉ
                        result = _projectDL.GetAll(input.DBDomain);
                    }
                    else
                    {
                        // Là nhân viên
                        var resDomain = _projectDL.GetAll(input.DBDomain);
                        var resCompany = _projectDL.GetAll(input.DBCompany);
                        result = resDomain.Concat(resCompany).ToList();
                    }
                }
            }
            return result;
        }
    }
}

using QVC.TASK.Common;
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
    }
}

using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;

namespace QVC.TASK.API.Controllers
{
    public class ProjectController : BaseController<Project>
    {
        #region Field

        private IProjectBL _projectBL;

        #endregion

        #region Constructor

        public ProjectController(IProjectBL projectBL) : base(projectBL)
        {
            _projectBL = projectBL;
        }

        #endregion

        #region Method



        #endregion
    }
}

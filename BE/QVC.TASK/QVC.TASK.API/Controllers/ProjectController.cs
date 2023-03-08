using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;

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

        [HttpPost]
        [Route("getall-project")]
        public IActionResult GetAllProject([FromBody] GetAllInput input)
        {
            try
            {
                var record = _projectBL.GetAllProject(input);

                //Xử lý kết quả trả về
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domaindb"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getall-project-byid")]
        public IActionResult GetAllProjectById([FromQuery] string id, string domain, string? search)
        {
            try
            {
                var record = _projectBL.GetAllById(id, domain);

                //Xử lý kết quả trả về
                if (record != null)
                {
                    if(search!= null)
                    {
                        record = record.Where(t => t.ProjectName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    }
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion
    }
}

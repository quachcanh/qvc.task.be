using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;

namespace QVC.TASK.API.Controllers
{
    public class DepartmentController : BaseController<Department>
    {
        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        public DepartmentController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

        #region Method


        [HttpPost]
        [Route("getall-department")]
        public IActionResult GetAllDepartment([FromBody] GetAllInput input)
        {
            try
            {
                var record = _departmentBL.GetAllDepartment(input);

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
        #endregion
    }
}

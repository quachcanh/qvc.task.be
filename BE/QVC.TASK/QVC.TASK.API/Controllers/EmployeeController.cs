using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;

namespace QVC.TASK.API.Controllers
{
    public class EmployeeController : BaseController<Employee>
    {
        #region Field

        private IEmployeeBL _employeerBL;

        #endregion

        #region Constructor

        public EmployeeController(IEmployeeBL employeerBL) : base(employeerBL)
        {
            _employeerBL = employeerBL;
        }

        #endregion

        /// <summary>
        /// API đăng ký tài khoản
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Employee record, DBType dbType)
        {
            try
            {
                var results = _employeerBL.Insert(record, dbType);

                // Xử lý kết quả trả về
                if (results > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, results);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}

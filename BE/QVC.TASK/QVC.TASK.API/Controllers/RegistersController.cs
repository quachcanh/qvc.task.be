using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using System.Resources;

namespace QVC.TASK.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class RegistersController : Controller
    {
        #region Field

        private IRegisterBL _registerBL;

        #endregion

        #region Constructor

        public RegistersController(IRegisterBL registerBL)
        {
            _registerBL = registerBL;
        }

        #endregion
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] Employee record)
        {
            try
            {
                var results = _registerBL.SignUp(record);

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

        [HttpPost]
        [Route("GetOne")]
        public IActionResult GetOneEmployee([FromQuery] Guid? id, string? username, string? email)
        {
            try
            {
                var results = _registerBL.GetOneEmployee(id, username, email);

                // Xử lý kết quả trả về
                if (results != null)
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

        [HttpPost]
        [Route("UpdateByUserName")]
        public IActionResult UpdateByUserNameEmployee([FromBody] Employee employee)
        {
            try
            {
                var results = _registerBL.UpdateByUserNameEmployee(employee);

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

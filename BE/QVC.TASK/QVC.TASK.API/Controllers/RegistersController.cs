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

        /// <summary>
        /// API đăng ký tài khoản
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
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

        /// <summary>
        /// API lấy thông tin người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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

        /// <summary>
        /// API cập nhật thông tin người dùng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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

        /// <summary>
        /// API tạo db theo domain
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDBDomain")]
        public IActionResult CreateDBDomain([FromBody] Employee record)
        {
            try
            {
                var results = _registerBL.CreateDBDomain(record);

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

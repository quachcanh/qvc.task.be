using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;

namespace QVC.TASK.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        #region Field

        private ILoginBL _loginBL;

        #endregion

        #region Constructor

        public LoginController(ILoginBL loginBL)
        {
            _loginBL = loginBL;
        }

        #endregion

        /// <summary>
        /// API Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult Login([FromBody] Login login)
        {
            try
            {
                var results = _loginBL.Login(login);

                // Xử lý kết quả trả về
                if (results.Info != null && results.AccessToken != null)
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

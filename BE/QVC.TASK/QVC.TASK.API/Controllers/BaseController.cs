using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;

namespace QVC.TASK.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: QVCANH (29/11/2022)
        [HttpGet]
        [Route("{domain}")]
        public IActionResult GetAll([FromRoute] string domain)
        {
            try
            {
                var record = _baseBL.GetAll(domain);

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
        [Route("project")]
        public IActionResult GetAll([FromQuery] string id, string domain)
        {
            try
            {
                var record = _baseBL.GetAllById(id, domain);

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
    }
}

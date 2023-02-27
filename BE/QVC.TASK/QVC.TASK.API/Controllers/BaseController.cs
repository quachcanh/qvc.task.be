using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;

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
        [Route("getall-byid")]
        public IActionResult GetAllById([FromQuery] string id, string domain)
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

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] DataInsert<T> dataInsert)
        {
            try
            {
                var record = _baseBL.Insert(dataInsert);

                //Xử lý kết quả trả về
                if (record > 0)
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
        [HttpPost]
        [Route("updateby-id")]
        public IActionResult Update([FromBody] DataInsert<T> data)
        {
            try
            {
                var record = _baseBL.Update(data);

                //Xử lý kết quả trả về
                if (record > 0)
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

        [HttpGet]
        [Route("getby-id")]
        public IActionResult GetByID([FromQuery] string id, string domaindb)
        {
            try
            {
                var record = _baseBL.GetById(id, domaindb);

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

        [HttpDelete]
        [Route("deleteby-id")]
        public IActionResult Delele([FromQuery] Guid id, string db)
        {
            try
            {
                var record = _baseBL.Delete(id, db);

                //Xử lý kết quả trả về
                if (record > 0)
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

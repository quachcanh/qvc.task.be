using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;

namespace QVC.TASK.API.Controllers
{
    public class JobController : BaseController<Job>
    {
        #region Field

        private IJobBL _jobBL;

        #endregion

        #region Constructor

        public JobController(IJobBL jobBL) : base(jobBL)
        {
            _jobBL = jobBL;
        }

        #endregion

        #region Method
        [HttpPost]
        [Route("insert-job")]
        public IActionResult InsertJob([FromBody] JobInput jobInput)
        {
            try
            {
                var record = _jobBL.InsertJob(jobInput);

                //Xử lý kết quả trả về
                if (record)
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
        [Route("job-report")]
        public IActionResult GetJobsComplete([FromBody] DataGetJob dataGetJob)
        {
            try
            {
                var record = _jobBL.GetJobsReports(dataGetJob);

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

        [HttpGet]
        [Route("job-report")]
        public IActionResult GetJobReportsInProject([FromQuery] Guid id, string dbDomain)
        {
            try
            {
                var record = _jobBL.GetJobReportsInProject(id, dbDomain);

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

        [HttpGet]
        [Route("job-todo")]
        public IActionResult GetJobsToDo([FromQuery] Guid id, string dbDomain)
        {
            try
            {
                var record = _jobBL.GetJobsToDo(id, dbDomain);

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

        [HttpGet]
        [Route("job-processing")]
        public IActionResult GetJobsProcessing([FromQuery] Guid id, string dbDomain)
        {
            try
            {
                var record = _jobBL.GetJobsProcessing(id, dbDomain);

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

        [HttpGet]
        [Route("job-complete")]
        public IActionResult GetJobsComplete([FromQuery] Guid id, string dbDomain)
        {
            try
            {
                var record = _jobBL.GetJobsComplete(id, dbDomain);

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

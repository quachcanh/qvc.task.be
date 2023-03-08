using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using QVC.TASK.Common.Enums;

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
        [Route("Insert-type")]
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

        [HttpGet]
        [Route("getall-user-dbinfo")]
        public IActionResult GetAllUserDBInfo([FromQuery] string dbDomain, string dbInfo, TypeGetEmp type)
        {
            try
            {
                List<Employee> results = new List<Employee>();
                var resdomain = _employeerBL.GetAll(dbDomain);
                var resinffo = _employeerBL.GetAll(dbInfo);
                //Xử lú
                if (resdomain?.Count > 0 || resinffo?.Count > 0)
                {
                    if (type == TypeGetEmp.Assign)
                    {
                        List<Employee> employees = resdomain.Concat(resinffo).ToList();
                        results = employees.GroupBy(e => e.EmployeeID).Select(g => g.First()).ToList();
                    }
                    else if (type == TypeGetEmp.Add)
                    {
                        HashSet<Guid> ids = new HashSet<Guid>(resdomain.Select(e => e.EmployeeID));
                        results = resinffo.Where(e => !ids.Contains(e.EmployeeID)).ToList();

                    }

                    if (results != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, results);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, results);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}

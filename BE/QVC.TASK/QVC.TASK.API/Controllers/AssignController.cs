using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;

namespace QVC.TASK.API.Controllers
{
    public class AssignController :BaseController<Assign>
    {
        #region Field

        private IAssignBL _assignBL;

        #endregion

        #region Constructor

        public AssignController(IAssignBL assignBL) : base(assignBL)
        {
            _assignBL = assignBL;
        }

        #endregion

        #region Method

        [HttpPost]
        [Route("GetAllMyTask")]
        public IActionResult GetAllMyTask(MyTaskInput input)
        {
            try
            {
                var record = _assignBL.GetAllMyTask(input);

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

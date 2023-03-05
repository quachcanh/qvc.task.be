using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.Common.Enums;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class AssignBL : BaseBL<Assign>, IAssignBL
    {
        #region Field

        private IAssignDL _assignDL;

        #endregion

        #region Constructor

        public AssignBL(IAssignDL assignDL) : base(assignDL)
        {
            _assignDL = assignDL;


            #endregion
        }

        public List<AllTask> GetAllMyTask(MyTaskInput input)
        {

            List<AllTask> all = new List<AllTask>();

            if (input != null)
            {
                //Kiểm tra là cá nhân hay công ty
                if (input.State == Common.Enums.State.CaNhan) // Cá nhân
                {
                    var task = _assignDL.GetAllMyTask(input.Id, input.DBDomain);
                    task = task.Where(t => t.State == Common.Enums.State.CaNhan).ToList();
                    // Lọc theo type công việc cần lấy
                    if (task?.Count > 0)
                    {
                        // Việc cần làm
                        if (input.Type == JobStatus.Processing)
                        {
                            all = task.Where(t => t.JobStatus == JobStatus.Processing).ToList();
                        }
                        // Việc đã hoàn thành
                        if (input.Type == JobStatus.Complete)
                        {
                            all = task.Where(t => t.JobStatus == JobStatus.Complete).ToList();
                        }
                        // Việc quá hạn
                        if (input.Type == JobStatus.OutOfDate)
                        {
                            // Lấy những công việc chưa hoàn thành
                            task = task.Where(t => t.JobStatus != JobStatus.Complete).ToList();
                            // Lấy những công việc có endtime nhỏ hơn ngày hiện tại
                            DateTime currentDateTime = DateTime.Now; // lấy thời điểm hiện tại

                            all = task.Where(t => t.EndTime != null && t.EndTime < currentDateTime).ToList();
                        }


                    }
                }
                else  // Công ty
                {
                    var taskdomain = _assignDL.GetAllMyTask(input.Id, input.DBDomain);
                    var taskcompany = _assignDL.GetAllMyTask(input.Id, input.DBCompany);
                    if (taskdomain?.Count > 0 || taskcompany?.Count > 0)
                    {
                        // Ghép 2 list với nhau
                        var task = taskdomain.Concat(taskcompany).ToList();
                        if (task?.Count > 0)
                        {
                            // Việc cần làm
                            if (input.Type == JobStatus.Processing)
                            {
                                all = task.Where(t => t.JobStatus == JobStatus.Processing).ToList();
                            }
                            // Việc đã hoàn thành
                            if (input.Type == JobStatus.Complete)
                            {
                                all = task.Where(t => t.JobStatus == JobStatus.Complete).ToList();
                            }
                            // Việc quá hạn
                            if (input.Type == JobStatus.OutOfDate)
                            {
                                // Lấy những công việc chưa hoàn thành
                                task = task.Where(t => t.JobStatus != JobStatus.Complete).ToList();
                                // Lấy những công việc có endtime nhỏ hơn ngày hiện tại
                                DateTime currentDateTime = DateTime.Now; // lấy thời điểm hiện tại
                                all = task.Where(t => t.EndTime != null && t.EndTime < currentDateTime).ToList();
                            }
                            // Việc giao cho tôi
                            if (input.Type == JobStatus.Assignment)
                            {
                                all = taskcompany;
                            }
                        }
                    }
                }
                // Sắp xếp lại công việc
                switch (input.TypeSort)
                {
                    case TypeSort.EndTimeASC:
                        // code block
                        all = all.OrderBy(t => t.EndTime).ToList();
                        break;
                    case TypeSort.EndTimeDESC:
                        // code block
                        all = all.OrderByDescending(t => t.EndTime).ToList();
                        break;
                    case TypeSort.StartTimeASC:
                        // code block
                        all = all.OrderBy(t => t.StartTime).ToList();
                        break;
                    case TypeSort.StartTimeDESC:
                        // code block
                        all = all.OrderByDescending(t => t.StartTime).ToList();
                        break;
                    case TypeSort.CreatedDateASC:
                        // code block
                        all = all.OrderBy(t => t.CreatedDate).ToList();
                        break;
                    case TypeSort.CreatedDateDESC:
                        // code block
                        all = all.OrderByDescending(t => t.CreatedDate).ToList();
                        break;
                    default:
                        // code block
                        break;
                }
            }
            return all;
        }
    }
}

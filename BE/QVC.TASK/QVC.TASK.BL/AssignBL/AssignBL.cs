using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.Common.Enums;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using FluentDateTime;

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
                        // Bỏ những công việc trùng nhau
                        task = task.GroupBy(t => t.JobID).Select(t => t.First()).ToList();
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

                // Lọc theo input search
                if (input.Search != null)
                {
                    all = all.Where(t => t.JobName.IndexOf(input.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.DepartmentName.IndexOf(input.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.ProjectName.IndexOf(input.Search, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();
                }
                // Lọc theo hạn hoàn thành
                if (input.DateOption != DateOption.None)
                {
                    switch (input.DateOption)
                    {
                        case DateOption.ToDay:
                            all = all.Where(t => t.EndTime?.Date == DateTime.Now.Date).ToList();
                            break;
                        case DateOption.ThisWeek:
                            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
                            DateTime mondayOfWeek = DateTime.Now.AddDays(-(int)dayOfWeek + 1);
                            DateTime sundayOfWeek = DateTime.Now.AddDays(7 - (int)dayOfWeek);
                            all = all.Where(t => t.EndTime?.Date >= mondayOfWeek.Date && t.EndTime?.Date <= sundayOfWeek.Date).ToList();
                            break;
                        case DateOption.LastWeek:
                            DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
                            DateTime sundayOfLastWeek = mondayOfLastWeek.AddDays(6);
                            all = all.Where(t => t.EndTime?.Date >= mondayOfLastWeek.Date && t.EndTime?.Date <= sundayOfLastWeek.Date).ToList();
                            break;
                        case DateOption.ThisMonth:
                            // Ngày đầu tiên của tháng
                            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            // Ngày cuối cùng của tháng
                            DateTime lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                            all = all.Where(t => t.EndTime?.Date >= firstDayOfMonth.Date && t.EndTime?.Date <= lastDayOfMonth.Date).ToList();
                            break;
                        case DateOption.LastMonth:
                            DateTime firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                            DateTime lastDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                            all = all.Where(t => t.EndTime?.Date >= firstDayOfLastMonth.Date && t.EndTime?.Date <= lastDayOfLastMonth.Date).ToList();
                            break;
                        case DateOption.Other:
                            if(input.StartDate!= null || input.EndDate != null)
                            {
                                if(input.StartDate != null && input.EndDate == null)
                                {
                                    all = all.Where(t => t.EndTime?.Date >= input.StartDate?.Date).ToList();
                                }
                                else if(input.StartDate == null && input.EndDate != null)
                                {
                                    all = all.Where(t => t.EndTime?.Date <= input.EndDate?.Date ).ToList();
                                }
                                else if(input.StartDate != null && input.EndDate != null)
                                {
                                    all = all.Where(t => t.EndTime?.Date >= input.StartDate?.Date && t.EndTime?.Date <= input.EndDate?.Date).ToList();
                                }
                            }
                            break;
                        default:
                            // code block
                            break;
                    }
                }
                else
                {

                }
            }
            return all;
        }
    }
}

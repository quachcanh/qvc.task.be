using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.Common.Enums;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class JobBL : BaseBL<Job>, IJobBL
    {
        #region Field

        private IJobDL _jobDL;

        #endregion

        #region Constructor

        public JobBL(IJobDL jobDL) : base(jobDL)
        {
            _jobDL = jobDL;
        }




        #endregion

        public List<JobOutput> GetAllJobByIdProject(JobAllInput input)
        {
            var all = _jobDL.GetAllJobByIdProject(input.Id, input.DBDomain);

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
                all = all.Where(t => t.JobName.IndexOf(input.Search, StringComparison.OrdinalIgnoreCase) >= 0 || t.EmpAssign?.IndexOf(input.Search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
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
                        if (input.StartDate != null || input.EndDate != null)
                        {
                            if (input.StartDate != null && input.EndDate == null)
                            {
                                all = all.Where(t => t.EndTime?.Date >= input.StartDate?.Date).ToList();
                            }
                            else if (input.StartDate == null && input.EndDate != null)
                            {
                                all = all.Where(t => t.EndTime?.Date <= input.EndDate?.Date).ToList();
                            }
                            else if (input.StartDate != null && input.EndDate != null)
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
            return all;
        }
        public JobReports GetJobReportsInProject(Guid id, string dbDomain)
        {
            JobReports jobs = new JobReports();

            jobs.Complete = _jobDL.GetJobsComplete(id, dbDomain).Count;
            jobs.Processing = _jobDL.GetJobsProcessing(id, dbDomain).Count;
            jobs.Todo = _jobDL.GetJobsToDo(id, dbDomain).Count;
            jobs.OutOfDate = _jobDL.GetJobsOutOfDate(id, dbDomain).Count;
            return jobs;
        }



        public JobReports GetJobsReports(DataGetJob dataGetJob)
        {
            JobReports jobs = new JobReports();
            if (dataGetJob != null && dataGetJob.Id?.Count > 0)
            {
                foreach (var item in dataGetJob.Id)
                {
                    jobs.Complete = jobs.Complete + _jobDL.GetJobsComplete(item, dataGetJob.DBDomain).Count;
                    jobs.Processing = jobs.Processing + _jobDL.GetJobsProcessing(item, dataGetJob.DBDomain).Count;
                    jobs.Todo = jobs.Todo + _jobDL.GetJobsToDo(item, dataGetJob.DBDomain).Count;
                    jobs.OutOfDate = jobs.OutOfDate + _jobDL.GetJobsOutOfDate(item, dataGetJob.DBDomain).Count;
                }
            }
            return jobs;
        }

        public List<Job> GetJobsComplete(Guid id, string domaindb)
        {
            return _jobDL.GetJobsComplete(id, domaindb);
        }

        public List<Job> GetJobsProcessing(Guid id, string domaindb)
        {
            return _jobDL.GetJobsProcessing(id, domaindb);
        }

        public List<Job> GetJobsToDo(Guid id, string domaindb)
        {
            return _jobDL.GetJobsToDo(id, domaindb);
        }


        /// <summary>
        /// THực hiện thêmm mới công việc bao gồm cả công việc con
        /// </summary>
        /// <param name="dataInsert">Công việc cha</param>
        /// <param name="jobs">Danh sách công việc con</param>
        /// <returns></returns>
        public bool InsertJob(JobInput jobInput)
        {
            // Thêm mới công việc cha
            DataInsert<Job> dataInsert = new DataInsert<Job>();
            dataInsert = jobInput.DataInsert;
            var results = Insert(dataInsert);
            bool res = true;
            if (results > 0)
            {
                // Thực hiện thêm mới công việc con
                DataInsert<Job> newJob = new DataInsert<Job>();
                if (jobInput.DataList?.Count > 0)
                {
                    foreach (var job in jobInput.DataList)
                    {
                        // Bưu dữ liệu
                        newJob.Data = job;
                        newJob.DBDomain = dataInsert.DBDomain;

                        // Thực hiện thêm mới
                        int row = Insert(newJob);
                        if (row <= 0)
                        {
                            res = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}

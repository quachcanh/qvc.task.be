using QVC.TASK.Common;
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

        public JobReports GetJobReportsInProject(Guid id, string dbDomain)
        {
            JobReports jobs = new JobReports();

            jobs.Complete = _jobDL.GetJobsComplete(id, dbDomain).Count;
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
            return res;
        }
    }
}

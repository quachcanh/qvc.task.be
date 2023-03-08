using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public interface IJobBL : IBaseBL<Job>
    {
        /// <summary>
        /// THực hiện thêmm mới công việc bao gồm cả công việc con
        /// </summary>
        /// <param name="dataInsert">Công việc cha</param>
        /// <param name="jobs">Danh sách công việc con</param>
        /// <returns></returns>
        public bool InsertJob(JobInput jobInput);

        public JobReports GetJobsReports(DataGetJob dataGetJob);

        public JobReports GetJobReportsInProject(Guid id, string dbDomain);

        public List<Job> GetJobsComplete(Guid id, string domaindb);

        public List<Job> GetJobsProcessing(Guid id, string domaindb);

        public List<Job> GetJobsToDo(Guid id, string domaindb);

        public List<JobOutput> GetAllJobByIdProject(JobAllInput input);
    }
}

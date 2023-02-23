using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public interface IJobDL : IBaseDL<Job>
    {
        /// <summary>
        /// Thực hiện thêm mới nhiều công việc
        /// </summary>
        /// <param name="jobs">Danh sách công việc</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        //public bool InsertJobChild(List<Job> jobs, IDbConnection connection, IDbTransaction transaction);

        public List<Job> GetJobsComplete(Guid id, string domaindb);

        public List<Job> GetJobsProcessing(Guid id, string domaindb);

        public List<Job> GetJobsToDo(Guid id, string domaindb);
        public List<Job> GetJobsOutOfDate(Guid id, string domaindb);
    }
}

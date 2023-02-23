using Dapper;
using MySqlConnector;
using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public class JobDL : BaseDL<Job>, IJobDL
    {
        /// <summary>
        /// Thực hiện thêm mới nhiều công việc
        /// </summary>
        /// <param name="jobs">Danh sách công việc</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        //public bool InsertJobChild(List<Job> jobs, IDbConnection connection, IDbTransaction transaction)
        //{
        //    // Chuẩn bị tên stored procedure
        //    string storedProcedureName = "Proc_Inserts_Job";

        //    // Chuẩn bị danh sách tham số đầu vào cho sở thích phục vụ
        //    var parameters = new DynamicParameters();
        //    string values = "";
        //    foreach (var item in jobs)
        //    {
        //        Guid newID = Guid.NewGuid();
        //        string value = "'" + item.JobID + "','" + item.JobCode + "','" + item.JobName + "','" + item.ProjectID + "','" + item.JobStatus+ "','"+item.JobTag+ "','"+item.StartTime+ "','"+item.EndTime+ "','"+item.Description+ "','"+item.ParentID+ "','"+                 "',NOW(),'',NOW(),''";
        //        values = values + "(" + value + ")" + ",";
        //    }

        //    // Thêm tham số đầu vào cho parameters
        //    parameters.Add("@Values", values.Remove(values.Length - 1));
        //}
        public List<Job> GetJobsComplete(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Complete_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<Job> GetJobsOutOfDate(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_OutOfDate_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<Job> GetJobsProcessing(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Processing_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<Job> GetJobsToDo(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Todo_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }
    }
}

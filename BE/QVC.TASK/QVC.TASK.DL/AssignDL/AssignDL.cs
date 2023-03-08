using Dapper;
using MySqlConnector;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public class AssignDL : BaseDL<Assign>, IAssignDL
    {
        /// <summary>
        /// Mở kết nối
        /// </summary>
        /// <param name="connection">Dối tượng đang kết nối</param>
        /// Created by: QVCANH (28/12/2022)
        public void OpenConnection(IDbConnection connection)
        {
            // Mở kết nối
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Đóng kết nối
        /// </summary>
        /// <param name="connection">Dối tượng đang kết nối</param>
        /// Created by: QVCANH (28/12/2022)
        public void CloseConnection(IDbConnection connection)
        {
            // Đóng kết nối
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public List<AllTask> GetAllMyTask(Guid id, string dbdomanin)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format("Proc_GetAll_MyTask");

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<AllTask> allTasks = new List<AllTask>();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, dbdomanin)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                allTasks = mySqlConnection.Query<AllTask>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return allTasks;
        }
    }
}

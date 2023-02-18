using Dapper;
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
    public class LoginDL : ILoginDL
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

        /// <summary>
        /// Kiểm tra login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Employee Login(Common.Entities.DTO.Login login)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetByUserPass_Employee";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            // Thêm tham số đầu vào cho parameters
            if (login.Email != null)
            {
                parameters.Add("@Email", login.Email);
                parameters.Add("@UserName", null);
            }
            if (login.Password != null)
            {
                parameters.Add("@UserName", login.Username);
                parameters.Add("@Email", null);
            }
            parameters.Add("@Password", login.Password);

            // Khởi tạo đối tượng muốn lấy
            var dataResult = new Employee();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnector.MySqlConnection(String.Format(Database.DBDomain, login.Username + "_qvc_task")))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                dataResult = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            };
            return dataResult;
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetUserNameByEmail(string email)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetUserByEmail_Employee";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            // Khởi tạo đối tượng muốn lấy
            Employee emp = new Employee();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnector.MySqlConnection(DatabaseContext.ConnectionDBInfoString))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                emp = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            };
            return emp;
        }

        /// <summary>
        /// LẤy thông tin công ty từ mã công ty
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Company GetCompanyByCode(string code)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetByCode_Company";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyCode", code);

            // Khởi tạo đối tượng muốn lấy
            Company res = new Company();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnector.MySqlConnection(DatabaseContext.ConnectionDBInfoString))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                res = mySqlConnection.QueryFirstOrDefault<Company>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            };
            return res;
        }
    }
}

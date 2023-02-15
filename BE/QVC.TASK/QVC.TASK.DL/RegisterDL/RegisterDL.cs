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
    public class RegisterDL : IRegisterDL
    {
        /// <summary>
        /// Thêm mới bản ghi thông tin user
        /// </summary>
        /// <param name="record">bản ghi</param>
        /// <returns></returns>
        public int SignUp(Employee record)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Insert_Employee";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            // Lấy toàn bộ property của class T
            var props = typeof(Employee).GetProperties();
            foreach (var item in props)
            {
                // Lấy tên của properties
                var propertyName = item.Name;

                // Lấy giá trị của properties
                var propertyValue = item.GetValue(record);
                if (propertyName == RecordInformationName.CREATED_DATE || propertyName == RecordInformationName.MODIFIED_DATE)
                {
                    // Thêm tham số đầu vào cho parameters
                    parameters.Add($"@{propertyName}", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            // Khởi tạo đối tượng muốn lấy
            int rowAffected = 0;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionDBInfoString))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Khởi tạo Transaction
                using var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    // Thực hiện gọi vào Database để chạy stored procedure
                    rowAffected = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                    // Kiểm tra kết quả
                    if (rowAffected > 0)
                    {
                        // Commit transaction
                        transaction.Commit();
                    }
                    else
                    {
                        // Rollback transaction
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi
                    Console.WriteLine(ex.Message);

                    // Rollback transaction
                    transaction.Rollback();
                }
                finally
                {
                    // Đóng kết nối
                    CloseConnection(mySqlConnection);
                }

                // Trả về số bản ghi bị ảnh hưởng
                return rowAffected;
            };
        }

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

        public Employee GetOneEmployee(Guid? id, string? username, string? email)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetOne_Employee";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            // Thêm tham số đầu vào cho parameters
            parameters.Add($"@EmployeeID", id);
            parameters.Add($"@UserName", username);
            parameters.Add($"@Email", email);

            // Khởi tạo đối tượng muốn lấy
            var dataResult = new Employee();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionDBInfoString))
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

        public int UpdateByUserNameEmployee(Employee record)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_UpdateByUserName_Employee";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            // Lấy toàn bộ property của class T
            var props = typeof(Employee).GetProperties();
            foreach (var item in props)
            {
                // Lấy tên của properties
                var propertyName = item.Name;

                // Lấy giá trị của properties
                var propertyValue = item.GetValue(record);
                if (propertyName == RecordInformationName.CREATED_DATE || propertyName == RecordInformationName.MODIFIED_DATE)
                {
                    // Thêm tham số đầu vào cho parameters
                    parameters.Add($"@{propertyName}", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            // Khởi tạo đối tượng muốn lấy
            int rowAffected = 0;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionDBInfoString))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Khởi tạo Transaction
                using var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    // Thực hiện gọi vào Database để chạy stored procedure
                    rowAffected = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                    // Kiểm tra kết quả
                    if (rowAffected > 0)
                    {
                        // Commit transaction
                        transaction.Commit();
                    }
                    else
                    {
                        // Rollback transaction
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi
                    Console.WriteLine(ex.Message);

                    // Rollback transaction
                    transaction.Rollback();
                }
                finally
                {
                    // Đóng kết nối
                    CloseConnection(mySqlConnection);
                }

                // Trả về số bản ghi bị ảnh hưởng
                return rowAffected;
            };
        }
    }
}

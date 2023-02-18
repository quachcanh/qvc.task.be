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
    public class BaseDL<T> : IBaseDL<T>
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
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng bản ghi</returns>
        /// Created by: QVCANH (28/11/2022)
        public List<T> GetAll(string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format("Proc_GetAll_{0}", typeof(T).Name);

            // Khai báo đối tượng muốn lấy
            List<T> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<T>(storedProcedureName, null, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domaindb"></param>
        /// <returns></returns>
        public List<T> GetAllById(string id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format("Proc_GetById_{0}", typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<T> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection (mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        /// <summary>
        /// >Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="dataInsert"></param>
        /// <returns></returns>
        public int Insert(DataInsert<T> dataInsert)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format("Proc_Insert_{0}", typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            // Lấy toàn bộ property của class T
            var props = typeof(T).GetProperties();

            foreach (var item in props)
            {
                // Lấy tên của properties
                var propertyName = item.Name;

                // Lấy giá trị của properties
                var propertyValue = item.GetValue(dataInsert.Data);
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
            using (var mySqlConnection = new MySqlConnector.MySqlConnection(String.Format(Database.DBDomain, dataInsert.DBDomain)))
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

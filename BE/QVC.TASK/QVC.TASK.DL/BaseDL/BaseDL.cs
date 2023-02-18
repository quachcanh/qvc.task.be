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
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<T>(storedProcedureName, null, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Mở kết nối
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
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
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Mở kết nối
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
            }

            //Trả về đối tượng Employee
            return record;
        }
    }
}

using QVC.TASK.Common;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Thêm mới bản ghi vào bảng nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên</param>
        /// <param name="dbType">Loại kết nối</param>
        /// <returns></returns>
        public int Insert(Employee employee, DBType dbType)
        {
            // Khởi tạo chuỗi kết nối
            string conn = String.Empty;
            switch (dbType)
            {
                case DBType.DBInfo:
                    conn = DatabaseContext.ConnectionDBInfoString;
                    break;
                case DBType.DBDomain:
                    conn = String.Format(Database.DBDomain, employee.UserName + "_qvc_task");
                    break;
            }
            if (conn != String.Empty)
            {
                return _employeeDL.Insert(employee, conn);
            }
            else
            {
                return 0;
            }
        }
    }
}

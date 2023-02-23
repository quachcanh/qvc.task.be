using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Thêm mới bản ghi vào bảng nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên</param>
        /// <param name="conn">Chuỗi kết nối</param>
        /// <returns></returns>
        public int Insert(Employee employee, string conn);
    }
}

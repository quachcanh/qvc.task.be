using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public interface IEmployeeBL
    {
        /// <summary>
        /// Thêm mới bản ghi vào bảng nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên</param>
        /// <param name="dbType">Loại kết nôsi</param>
        /// <returns></returns>
        public int Insert(Employee employee, DBType dbType);
    }
}

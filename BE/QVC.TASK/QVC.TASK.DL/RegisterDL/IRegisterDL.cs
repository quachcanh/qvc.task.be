using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public interface IRegisterDL
    {
        /// <summary>
        /// Thêm mới bản ghi thông tin user
        /// </summary>
        /// <param name="record">bản ghi</param>
        /// <returns></returns>
        public int SignUp(Employee record);

        /// <summary>
        /// Chuyển thông tin nhân viên sang db domain
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int SyncEmployeeToDomain(Employee record);

        /// <summary>
        /// Lấy thông tin nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetOneEmployee(Guid? id, string? username, string? email);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int UpdateByUserNameEmployee(Employee record);

        /// <summary>
        /// Thực hiện tạo db theo doamin
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int CreateDBDomain(Employee record);
    }
}

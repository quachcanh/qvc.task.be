using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public interface ILoginDL
    {
        /// <summary>
        /// Kiểm tra login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Employee Login(Common.Entities.DTO.Login login);

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetUserNameByEmail(string email);

        /// <summary>
        /// LẤy thông tin công ty từ mã công ty
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Company GetCompanyByCode(string code);
    }
}

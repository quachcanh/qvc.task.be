using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common.Entities.DTO
{
    /// <summary>
    /// Thông tin tài khoản login
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Mã công ty
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// PAss
        /// </summary>
        public string Password { get; set; }
    }
}

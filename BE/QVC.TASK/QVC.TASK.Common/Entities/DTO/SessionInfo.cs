using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common.Entities.DTO
{
    public class SessionInfo
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Thông tin người dùng
        /// </summary>
        public Employee Info { get; set; }
    }
}

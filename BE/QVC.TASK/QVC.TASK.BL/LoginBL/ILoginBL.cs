using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public interface ILoginBL
    {
        /// <summary>
        /// Kiểm tra login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public SessionInfo Login(Common.Entities.DTO.Login login);
    }
}

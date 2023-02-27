using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion
        #region Constructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }


        #endregion

        public List<Department> GetAllDepartment(GetAllInput input)
        {
            List<Department> result = new List<Department>();
            if (input != null)
            {
                // Là cá nhân
                if (input.State == Common.Enums.State.CaNhan)
                {
                    result = _departmentDL.GetAll(input.DBDomain);
                }
                else
                {
                    // Là công ty
                    if (input.DBDomain == input.DBCompany)
                    {
                        // Là chính chỉ
                        result = _departmentDL.GetAll(input.DBDomain);
                    }
                    else
                    {
                        // Là nhân viên
                        var resDomain = _departmentDL.GetAll(input.DBDomain);
                        var resCompany = _departmentDL.GetAll(input.DBCompany);
                        if (resCompany?.Count > 0 || resDomain?.Count > 0)
                        {
                            resDomain = resDomain.Where(x => x.CompanyID == null).ToList();
                            resCompany = resCompany.Where(x => x.CompanyID != null).ToList();
                            result = resDomain.Concat(resCompany).ToList();
                        }
                    }
                }
            }
            return result;
        }
    }

}

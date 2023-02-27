using QVC.TASK.Common;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL.CompanyBL
{
    public class CompanyBL : BaseBL<Company>, ICompanyBL
    {
        #region Field

        private ICompanyDL _companyDL;

        #endregion

        #region Constructor

        public CompanyBL(ICompanyDL companyDL) : base(companyDL)
        {
            _companyDL = companyDL;
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using QVC.TASK.BL;
using QVC.TASK.Common;

namespace QVC.TASK.API.Controllers
{
    public class CompanyController : BaseController<Company>
    {
        #region Field

        private ICompanyBL _companyBL;

        #endregion

        #region Constructor

        public CompanyController(ICompanyBL companyBL) : base(companyBL)
        {
            _companyBL = companyBL;
        }

        #endregion

        #region Method



        #endregion
    }
}

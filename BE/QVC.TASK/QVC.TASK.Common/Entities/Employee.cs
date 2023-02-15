using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.Common
{
    public class Employee : Base
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? DepartmentID { get; set; }
        public Guid? PositionID { get; set; }
        public int Code { get; set; }
        public Employee()
        {
            EmployeeID= Guid.NewGuid();
        }
    }
}

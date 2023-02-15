using QVC.TASK.Common;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace QVC.TASK.BL
{
    public class RegisterBL : IRegisterBL
    {
        #region Field

        private IRegisterDL _registerDL;

        #endregion

        #region Constructor

        public RegisterBL(IRegisterDL registerDL)
        {
            _registerDL = registerDL;
        }

        #endregion
        public int SignUp(Employee record)
        {
            // Sinh mã xác thực tài khoản
            Random random = new Random();
            record.Code = random.Next(100000, 999999);
            int result = _registerDL.SignUp(record);

            // Gửi Email xác thực
            if (result > 0)
            {
                SentEmail(record);
            }
            return result;
        }

        public void SentEmail(Employee record)
        {
            try
            {
                
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("canhquach01061999@gmail.com");
                mail.To.Add(record.Email);
                mail.Subject = "MÃ XÁC THỰC TÀI KHOẢN QVC TASK";
                mail.Body = $"Xin chào {record.EmployeeName},\nMã xác thực của bạn là:\n{record.Code}\nVui lòng xác thực tài khoản theo mã này.";


                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("canhquach01061999@gmail.com", "ljedyrvztvhkolpl");
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Employee GetOneEmployee(Guid? id, string? username, string? email)
        {
            return _registerDL.GetOneEmployee(id, username, email);
        }

        public int UpdateByUserNameEmployee(Employee record)
        {
            return _registerDL.UpdateByUserNameEmployee(record);
        }
    }
}

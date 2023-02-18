using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class LoginBL : ILoginBL
    {
        #region Field

        private ILoginDL _loginDL;

        #endregion

        #region Constructor

        public LoginBL(ILoginDL loginDL)
        {
            _loginDL = loginDL;
        }

        #endregion

        /// <summary>
        /// Kiểm tra login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public SessionInfo Login(Login login)
        {
            // Khởi tạo đối tượng thông tin người dùng
            Employee employee = new Employee();
            SessionInfo session = new SessionInfo();

            if (login != null)
            {
                //  Nếu đăng nhập là công ty
                if (!string.IsNullOrEmpty(login.CompanyCode))
                {
                    // Lấy thông tin công ty để truy cập vào db
                    var res = _loginDL.GetCompanyByCode(login.CompanyCode);
                    if (res != null && res.CreatedBy != null)
                    {
                        login.Username = res.CreatedBy;
                        employee = _loginDL.Login(login);
                    }
                }
                else  // Đăng nhập là cá nhân
                {
                    // Nếu nhập email
                    if (login.Email != null)
                    {
                        var user = _loginDL.GetUserNameByEmail(login.Email);
                        if (user != null)
                        {
                            login.Username = user.UserName;
                            employee = _loginDL.Login(login);
                        }
                    }
                    else // Nhập username
                    {
                        employee = _loginDL.Login(login);
                    }
                }

                // Nếu đăng nhập thành công -> tạo token
                if(employee != null)
                {
                    string accessToken = CreateToken(employee);
                    session.AccessToken = accessToken;
                    session.Info= employee;
                }
            }
            return session;
        }

        /// <summary>
        /// Tạo Access Token
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private string CreateToken(Employee employee)
        {
            // Chuyển thông tin sang json
            var data = JsonConvert.SerializeObject(employee);

            // Tạo một danh sách các claim (trường) của token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,employee.UserName),
                new Claim(JwtRegisteredClaimNames.Email,employee.Email),
                new Claim(JwtRegisteredClaimNames.Acr,data),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Tạo một khóa bí mật
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qvc-task"));

            // Tạo một đối tượng SigningCredentials để xác thực token bằng khóa bí mật
            var key = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(key);
            }
            var signingKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Tạo một đối tượng JwtSecurityToken để đại diện cho token
            var token = new JwtSecurityToken(
                issuer: "qvcanh",
                audience: "user",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(300),
                signingCredentials: credentials
            );

            // Lấy chuỗi của token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}

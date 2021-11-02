using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        string Register(RegisterModel userData);
        string EncryptPassWord(string password);

        RegisterModel Login(LoginModel loginData);
    }
}

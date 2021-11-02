using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);

        RegisterModel Login(LoginModel loginData);
    }
}

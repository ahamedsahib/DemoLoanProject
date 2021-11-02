using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class UserManager :IUserManager
    {
       
        private readonly IUserRepository repoistory;

      
        public UserManager(IUserRepository repoistory)
        {
            this.repoistory = repoistory;
        }

  
        public string Register(RegisterModel userData)
        {
            try
            {
                return this.repoistory.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public RegisterModel Login(LoginModel loginData) 
        {
            try
            {
                return this.repoistory.Login(loginData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
       
        private readonly IUserRepository userrepoistory;

      
        public UserManager(IUserRepository userrepoistory)
        {
            this.userrepoistory = userrepoistory;
        }

  
        public string Register(RegisterModel userData)
        {
            try
            {
                return this.userrepoistory.Register(userData);
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
                return this.userrepoistory.Login(loginData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RegisterModel GetUserDetails(int userId)
        {
            try
            {
                return this.userrepoistory.GetUserDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}

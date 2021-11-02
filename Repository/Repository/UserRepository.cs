using Microsoft.Extensions.Configuration;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.Configuration = configuration;
        }

 
        private IConfiguration Configuration { get; }

       
        public string Register(RegisterModel userData)
        {
            try
            {
                var check = this.userContext.UsersData.Where(x => x.EmailId == userData.EmailId).FirstOrDefault();
                if (check == null)
                {
                    if (userData != null)
                    {
                        userData.Password = this.EncryptPassWord(userData.Password);
                        this.userContext.UsersData.Add(userData);
                        this.userContext.SaveChanges();
                        return "Registration Successful";
                    }

                    return "Registration UnSuccessful";
                }

                return "Registration UnSuccessful";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }


        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                // string message;
                string encodedPassword = this.EncryptPassWord(loginData.Password);
                var login = this.userContext.UsersData.Where(x => x.EmailId == loginData.EmailId && x.Password == encodedPassword).FirstOrDefault();

                if (login == null)
                {
                    return null;
                }
                else
                {
                 
                    return login;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public RegisterModel GetUserDetails(int userId)
        {
            try
            {
                var userDetails = this.userContext.UsersData.Where(a => a.UserId == userId).SingleOrDefault();
              

             if(userDetails!=null)
                {
                    return userDetails;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
    public class PropertyRepository: IPropertyRepository
    {
       
        private readonly UserContext userContext;

        private readonly IConfiguration configuration;
        public PropertyRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

     
        public PropertyModel AddProperty(PropertyModel propertyData)
        {
            try
            {
                if (propertyData!=null)
                {
                    this.userContext.Property.Add(propertyData);
                    this.userContext.SaveChanges();
                    return propertyData;

                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<PropertyModel> GetPropertyDetails(int userId)
        {
            try
            {
                var propertyData = this.userContext.Property.Where(a => a.UserId == userId).ToList();
                if (propertyData.Count>0)
                {
                    return propertyData;
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

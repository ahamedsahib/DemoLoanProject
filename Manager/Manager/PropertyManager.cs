using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class PropertyManager: IPropertyManager
    {
        private readonly IPropertyRepository propertyRepository;


        public PropertyManager(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public FormModel AddForm(FormList formList)
        {
            try
            {
                return this.propertyRepository.AddForm(formList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string  AddProperty(List<PropertyModel> propertyData, int formId, int userId)
        {
            try
            {
                return this.propertyRepository.AddProperty(propertyData,formId, userId);
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
                return this.propertyRepository.GetPropertyDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

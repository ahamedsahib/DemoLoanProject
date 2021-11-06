using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IPropertyRepository
    {
        string AddProperty(List<PropertyModel> propertyData, int formId, int userId);

        List<PropertyModel> GetPropertyDetails(int userId);

        FormModel AddForm(FormList formList);
    }
}

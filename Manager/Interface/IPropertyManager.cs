using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IPropertyManager
    {
        string AddProperty(List<PropertyModel> propertyData, int formId, int userId);
        List<PropertyModel> GetPropertyDetails(int userId);
        FormModel AddForm(FormList formList);
    }
}

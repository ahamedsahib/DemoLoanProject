using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IPropertyManager
    {
        PropertyModel AddProperty(PropertyModel propertyData);

        List<PropertyModel> GetPropertyDetails(int userId);
    }
}

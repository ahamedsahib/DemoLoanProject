using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IPropertyRepository
    {
        PropertyModel AddProperty(PropertyModel propertyData);

        List<PropertyModel> GetPropertyDetails(int userId);
    }
}

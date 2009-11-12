using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OpenRasta;

namespace Jamaica.Validation
{
    public static class ValidationExtensions
    {
        public static IList<Error> ValidationErrors<T>(this T obj)
        {
            var errors = from property in TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(obj))
                         select new Error {Message = attribute.ErrorMessage};

            return errors.ToList();
        }
    }
}
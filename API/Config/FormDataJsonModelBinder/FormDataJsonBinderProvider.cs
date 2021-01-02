using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace BookArchive.API.Config
{
    public class FormDataJsonBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Do not use this provider for binding simple values
            var isSimpleValue = (!context.Metadata.IsComplexType);

            // Do not use this provider if the binding target is not a property
            var propName = context.Metadata.PropertyName;
            var propInfo = context.Metadata.ContainerType?.GetProperty(propName);
            var isNotProp = (propName == null || propInfo == null);
            var isNotParam = context.Metadata.ParameterName==null;
            Type type = null;
            if (isNotProp && isNotParam)
                return null;
            else type = isNotParam
                    ? propInfo.PropertyType
                    : context.Metadata.ModelType;

            // Do not use this provider if the target property type implements IFormFile
            if (isNotParam && !type.IsAssignableFrom(typeof(IFormFile))) return null;

            // Do not use this provider if this property does not have the FromForm attribute
            if(isNotParam && (!propInfo.PropertyType.GetCustomAttributes(typeof(FromFormAttribute), false).Any())) return null;
            if (isNotProp && !context.Metadata.ModelType.GetCustomAttributes(typeof(FromFormAttribute), false).Any()) return null;

            // All criteria met; use the FormDataJsonBinder
            return new FormDataJsonBinder();
        }
    }

}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Auth.Api.Models
{
    public class ValidationError
    {
        public string Message { get; private set; }
        public string Property { get; private set; }

        public ValidationError(ModelStateEntry erroredProperty, string propertyName)
        {
            Message = string.Join("\n", erroredProperty.Errors.Select(e => e.ErrorMessage));           
						Property = propertyName;
        }
    }
}

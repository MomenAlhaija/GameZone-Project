using GameZone.Settings;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedExtensionAttribute:ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                var extension=Path.GetExtension(file.FileName);
                if(!_allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"Only {_allowedExtensions} are allowed");
                }
            }
            return ValidationResult.Success;
        }
    }
}

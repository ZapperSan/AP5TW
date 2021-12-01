using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class ContentTypeAttribute : ValidationAttribute, IClientModelValidator
    {
        public string ContentType { get; set; }
        const string ErrorMess = "File doesn't have the appropriate content!";
        public ContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            else if (value is IFormFile formFile)
            {
                if (formFile.ContentType.ToLower().Contains(ContentType.ToLower())) return ValidationResult.Success;
                return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });
            }
            throw new NotImplementedException($"Validation is not supported for this type: {value.GetType()}");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-filecontent", ErrorMess);
            context.Attributes.Add("data-val-filecontent-type", ContentType);
        }
    }
}

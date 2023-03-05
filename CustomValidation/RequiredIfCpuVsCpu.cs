using Rock_Paper_Scissors.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Rock_Paper_Scissors.ViewModels
{
    public class RequiredIfCpuVsCpu : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (IndexViewModel)validationContext.ObjectInstance;
            bool isCpuVsCpu = model.IsCpuVsCpu;

            if (isCpuVsCpu && value == null)
            {
                return new ValidationResult("You must select a difficulty for the 2nd CPU player.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

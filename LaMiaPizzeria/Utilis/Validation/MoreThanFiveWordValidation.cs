using System.ComponentModel.DataAnnotations;

namespace LaMiaPizzeria.Utilis.Validation
{
    public class MoreThanFiveWordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(
               object value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue == null || fieldValue.Trim().Split(' ').Length - 1 <= 5)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }

            return ValidationResult.Success;
        }

    }
}

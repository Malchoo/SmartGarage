using System.ComponentModel.DataAnnotations;

public class RequiredIfNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult($"The {validationContext.DisplayName} field is required.");
    }
}
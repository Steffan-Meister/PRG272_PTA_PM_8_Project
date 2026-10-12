using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    /// <summary>
    /// Outcome of validating the Add Animal form. When <see cref="IsValid"/> is
    /// false, <see cref="Field"/> names the field in error and <see cref="Message"/>
    /// explains the problem.
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public AnimalField Field { get; private set; }
        public string Message { get; private set; }

        /// <summary>
        /// Private constructor; use <see cref="Valid"/> or <see cref="Invalid"/>.
        /// </summary>
        private ValidationResult(bool isValid, AnimalField field, string message)
        {
            IsValid = isValid;
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Creates the result marking the input as valid.
        /// </summary>
        public static ValidationResult Valid()
        {
            return new ValidationResult(true, AnimalField.AnimalID, null);
        }

        /// <summary>
        /// Creates the result marking the input as invalid and naming the field.
        /// </summary>
        public static ValidationResult Invalid(AnimalField field, string message)
        {
            return new ValidationResult(false, field, message);
        }
    }
}

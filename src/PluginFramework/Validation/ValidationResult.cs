namespace PluginFramework.Validation
{
    public class ValidationResult
    {
        public bool HasPassed { get; private set; }

        public ErrorResult Error { get; private set; }

        private ValidationResult()
        {
        }

        public static ValidationResult Passed()
        {
            return new ValidationResult
            {
                HasPassed = true
            };
        }

        public static ValidationResult Failed(string reason, string detail = null)
        {
            return new ValidationResult
            {
                HasPassed = false,
                Error = new ErrorResult(reason, detail)
            };
        }
    }
}

namespace PluginFramework.Validation
{
    public interface IValidationRule
    {
        ValidationResult Validate<TInput, TOutput>(IPlugin<TInput, TOutput> plugin);
    }
}

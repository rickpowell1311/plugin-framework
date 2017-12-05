namespace PluginFramework.Validation
{
    public interface IValidationRule
    {
        void Validate<TInput, TOutput>(IPluginDefinition<TInput, TOutput> pluginDefintion);
    }
}

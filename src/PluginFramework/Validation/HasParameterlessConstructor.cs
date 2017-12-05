using System.Linq;

namespace PluginFramework.Validation
{
    public class HasParameterlessConstructor : IValidationRule
    {
        public void Validate<TInput, TOutput>(IPluginDefinition<TInput, TOutput> pluginDefintion)
        {
            var hasParameterlessConstructor = !pluginDefintion.GetType().GetConstructors()
                .Any(c => !c.GetParameters().Any());

            if (hasParameterlessConstructor)
            {
                throw new InvalidPluginDefinitionException("All plugins should have a parameterless constructor");
            }
        }
    }
}

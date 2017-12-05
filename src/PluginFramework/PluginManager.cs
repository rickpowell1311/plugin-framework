using PluginFramework.Validation;
using System;
using System.Linq;
using System.Reflection;

namespace PluginFramework
{
    public static class PluginManager
    {
        public static PluginDefinitionManager<TInput, TOutput> ForDefintion<TInput, TOutput>(IPluginDefinition<TInput, TOutput> pluginDefinition)
        {
            return new PluginDefinitionManager<TInput, TOutput>(pluginDefinition);
        }
    }

    public class PluginDefinitionManager<TInput, TOutput>
    {
        private readonly IPluginDefinition<TInput, TOutput> pluginDefinition;

        public PluginDefinitionManager(IPluginDefinition<TInput, TOutput> pluginDefinition)
        {
            this.pluginDefinition = pluginDefinition;
        }

        public void Validate()
        {
            var currentAssembly = typeof(IValidationRule).GetTypeInfo().Assembly;
            var validatorTypes = currentAssembly.GetTypes()
                .Where(t => t.IsClass
                && typeof(IValidationRule).IsAssignableFrom(t));

            foreach (var validatorType in validatorTypes)
            {
                var validator = (IValidationRule)Activator.CreateInstance(validatorType);
                validator.Validate(pluginDefinition);
            }
        }
    }
}

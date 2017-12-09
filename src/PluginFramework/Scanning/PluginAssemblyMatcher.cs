using PluginFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PluginFramework.Scanning
{
    public class PluginAssemblyMatcher<TInput, TOutput> : IPluginAssemblyMatcher<TInput, TOutput>
    {
        private Validator validator;

        internal PluginAssemblyMatcher(Validator validator)
        {
            this.validator = validator;
        }

        public IEnumerable<IPlugin<TInput, TOutput>> FindMatchingPlugins(Assembly assembly, out Dictionary<Type, IEnumerable<ErrorResult>> validationErrors)
        {
            var validPlugins = new List<IPlugin<TInput, TOutput>>();
            var errors = new Dictionary<Type, IEnumerable<ErrorResult>>();
            var pluginTypes = assembly.GetTypes()
                .Where(t => typeof(IPlugin<TInput, TOutput>).IsAssignableFrom(t));

            foreach (var pluginType in pluginTypes)
            {
                if (!pluginType.GetConstructors()
                    .Any(c => !c.GetParameters().Any()))
                {
                    errors[pluginType] = new List<ErrorResult>
                    {
                        new ErrorResult("No parameterless constructor")
                    };

                    continue;
                }

                var test = Activator.CreateInstance(pluginType);
                var plugin = (IPlugin<TInput, TOutput>)Activator.CreateInstance(pluginType);
                var pluginErrors = validator.Validate(plugin);

                if (pluginErrors.Any())
                {
                    errors[pluginType] = pluginErrors;
                    continue;
                }

                validPlugins.Add(plugin);
            }

            validationErrors = errors;
            return validPlugins;
        }
    }
}

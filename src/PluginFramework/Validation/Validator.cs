using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PluginFramework.Validation
{
    public class Validator
    {
        private IEnumerable<IValidationRule> validationRules;

        internal Validator()
        {
            var validationTypes = typeof(Validator).GetTypeInfo().Assembly.GetTypes()
                .Where(t => typeof(IValidationRule).IsAssignableFrom(t)
                    && t.IsClass && t.GetConstructors().Any(c => !c.GetParameters().Any()));

            validationRules = validationTypes.Select(vt => (IValidationRule)Activator.CreateInstance(vt));
        }

        public IEnumerable<ErrorResult> Validate<TInput, TOutput>(IPlugin<TInput, TOutput> plugin)
        {
            foreach (var validationRule in validationRules)
            {
                var validationResult = validationRule.Validate(plugin);
                if (!validationResult.HasPassed)
                {
                    yield return validationResult.Error;
                }
            }
        }
    }
}

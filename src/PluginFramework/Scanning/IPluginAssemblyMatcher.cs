using PluginFramework.Validation;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PluginFramework.Scanning
{
    public interface IPluginAssemblyMatcher<TInput, TOutput>
    {
        IEnumerable<IPlugin<TInput, TOutput>> FindMatchingPlugins(Assembly assembly, out Dictionary<Type, IEnumerable<ErrorResult>> validationErrors);
    }
}

using System;
using System.Collections.Generic;

namespace PluginFramework
{
    public interface IPluginManager<TInput, TOutput>
    {
        IEnumerable<IPlugin<TInput, TOutput>> Plugins { get; }

        Dictionary<string, IEnumerable<ErrorResult>> PluginAssemblyLoadErrors { get; }

        Dictionary<Type, IEnumerable<ErrorResult>> PluginValidationErrors { get; }

        void ScanForPlugins(string directory, string assemblyName);
    }
}

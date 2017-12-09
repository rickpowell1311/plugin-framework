using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace PluginFramework.Scanning
{
    public class PluginScanner<TInput, TOutput>
    {
        private readonly IPluginAssemblyMatcher<TInput, TOutput> pluginAssemblyMatcher;

        internal Dictionary<string, IEnumerable<ErrorResult>> PluginAssemblyLoadErrors { get; private set; }

        internal Dictionary<Type, IEnumerable<ErrorResult>> PluginValidationErrors { get; private set; }

        internal PluginScanner(IPluginAssemblyMatcher<TInput, TOutput> pluginAssemblyMatcher)
        {
            this.pluginAssemblyMatcher = pluginAssemblyMatcher;

            PluginAssemblyLoadErrors = new Dictionary<string, IEnumerable<ErrorResult>>();
            PluginValidationErrors = new Dictionary<Type, IEnumerable<ErrorResult>>();
        }

        internal IEnumerable<IPlugin<TInput, TOutput>> ScanDirectory(string directory, string assemblyName)
        {
            PluginAssemblyLoadErrors = new Dictionary<string, IEnumerable<ErrorResult>>();
            PluginValidationErrors = new Dictionary<Type, IEnumerable<ErrorResult>>();

            var directoryInfo = new DirectoryInfo(directory);

            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException(directory);
            }

            var pluginCollection = new List<IPlugin<TInput, TOutput>>();

            foreach (var file in directoryInfo
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(f => f.Extension == ".dll"
                    && f.Name == assemblyName))
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    var plugins = pluginAssemblyMatcher.FindMatchingPlugins(assembly, out var pluginValidationErrors);

                    foreach (var validationError in pluginValidationErrors)
                    {
                        PluginValidationErrors[validationError.Key] = pluginValidationErrors[validationError.Key];
                    }

                    pluginCollection.AddRange(plugins);
                }
                catch (Exception ex)
                {
                    PluginAssemblyLoadErrors.Add(file.FullName, new List<ErrorResult> { new ErrorResult(ex.Message, ex.StackTrace) });
                }
            }

            return pluginCollection;
        }
    }
}

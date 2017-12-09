using Autofac;
using PluginFramework.IoC;
using PluginFramework.Scanning;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PluginFramework.Tests")]

namespace PluginFramework
{
    public sealed class PluginManager<TInput, TOutput> : IPluginManager<TInput, TOutput>
    {
        private readonly PluginScanner<TInput, TOutput> scanner;

        private IEnumerable<IPlugin<TInput, TOutput>> plugins;

        public IEnumerable<IPlugin<TInput, TOutput>> Plugins
        {
            get
            {
                return plugins;
            }
        }

        public Dictionary<string, IEnumerable<ErrorResult>> PluginAssemblyLoadErrors
        {
            get
            {
                return scanner.PluginAssemblyLoadErrors;
            }
        }

        public Dictionary<Type, IEnumerable<ErrorResult>> PluginValidationErrors
        {
            get
            {
                return scanner.PluginValidationErrors;
            }
        }
        internal PluginManager(PluginScanner<TInput, TOutput> scanner)
        {
            this.scanner = scanner;

            this.plugins = new List<IPlugin<TInput, TOutput>>();
        }

        public void ScanForPlugins(string directory, string assemblyName)
        {
            this.plugins = scanner.ScanDirectory(directory, assemblyName);
        }
    }

    public static class PluginManager
    {
        public static IPluginManager<TInput, TOutput> ForPluginTypes<TInput, TOutput>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterPluginFrameworkDependencies();

            return (IPluginManager<TInput, TOutput>)builder.Build().Resolve(typeof(IPluginManager<TInput, TOutput>));
        }
    }
}

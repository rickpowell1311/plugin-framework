using Autofac;
using Autofac.Core.Activators.Reflection;
using PluginFramework.Scanning;
using PluginFramework.Validation;
using System;
using System.Linq;
using System.Reflection;

namespace PluginFramework.IoC
{
    internal static class AutofacContainer
    {
        internal static void RegisterPluginFrameworkDependencies(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(PluginManager<,>)).As(typeof(IPluginManager<,>))
                .FindConstructorsWith(new InternalConstructorFinder());
            builder.RegisterGeneric(typeof(PluginScanner<,>)).AsSelf()
                .FindConstructorsWith(new InternalConstructorFinder());
            builder.RegisterGeneric(typeof(PluginAssemblyMatcher<,>)).As(typeof(IPluginAssemblyMatcher<,>))
                .FindConstructorsWith(new InternalConstructorFinder());
            builder.RegisterType<Validator>().AsSelf()
                .FindConstructorsWith(new InternalConstructorFinder());
        }

        internal class InternalConstructorFinder : IConstructorFinder
        {
            public ConstructorInfo[] FindConstructors(Type t) => 
                t.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsPrivate && !c.IsPublic).ToArray();
        }
    }
}

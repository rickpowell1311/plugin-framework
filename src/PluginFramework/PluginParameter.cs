using System;

namespace PluginFramework
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class PluginParameter : Attribute
    {
        public string Name { get; set;  }

        public PluginParameter()
        {
        }
    }
}

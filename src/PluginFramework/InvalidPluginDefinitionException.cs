using System;

namespace PluginFramework
{
    [Serializable]
    public class InvalidPluginDefinitionException : Exception
    {
        public InvalidPluginDefinitionException(string message)
            : base(message)
        {
        }

        public InvalidPluginDefinitionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

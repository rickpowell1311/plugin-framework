using PluginFramework.Validation;
using Xunit;

namespace PluginFramework.Tests.Validation
{
    public class HasParameterlessConstructorTests
    {
        [Fact]
        public void WhenPluginDoesNotHaveParameterlessConstructor_ThrowsInvalidPluginDefinitionException()
        {
            Assert.Throws<InvalidPluginDefinitionException>(() => new HasParameterlessConstructor().Validate(new NonParameterlessConstructorPlugin("test")));
        }

        [Fact]
        public void WhenPluginHasParameterlessConstructor_DoesNotThrowInvalidPluginDefinition()
        {
            new HasParameterlessConstructor().Validate(new ParameterlessConstructorPlugin());
        }
    }

    public class NonParameterlessConstructorPlugin : IPluginDefinition<Input, Output>
    {
        public NonParameterlessConstructorPlugin(string something)
        {
        }

        public Output Execute(Input input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ParameterlessConstructorPlugin : IPluginDefinition<Input, Output>
    {
        public ParameterlessConstructorPlugin()
        {

        }

        public Output Execute(Input input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Input
    {
    }

    public class Output
    {
    }
}

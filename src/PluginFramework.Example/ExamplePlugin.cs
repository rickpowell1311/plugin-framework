namespace PluginFramework.Example
{
    public class ExamplePlugin : IPlugin<ExampleInput, ExampleOutput>
    {
        public ExampleOutput Execute(ExampleInput input)
        {
            return new ExampleOutput { HasExecuted = true };
        }
    }
}

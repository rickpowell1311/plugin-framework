namespace PluginFramework
{
    public interface IPluginDefinition<TInput, TOutput>
    {
        TOutput Execute(TInput input);
    }
}

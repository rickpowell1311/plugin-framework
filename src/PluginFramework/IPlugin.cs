namespace PluginFramework
{
    public interface IPlugin<TInput, TOutput>
    {
        TOutput Execute(TInput input);
    }
}

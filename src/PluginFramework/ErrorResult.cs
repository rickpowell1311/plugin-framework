namespace PluginFramework
{
    public class ErrorResult
    {
        public string Reason { get; }

        public string Detail { get; }

        internal ErrorResult(string reason, string detail = null)
        {
            Reason = reason;
            Detail = detail;
        }
    }
}

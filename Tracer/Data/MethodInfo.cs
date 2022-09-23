
namespace Tracer
{
    [Serializable]
    public class MethodInfo
    {

        public string? ClassName { get; internal set; }

        public string? MethodName { get; internal set; }

        public long Time { get; internal set; }

    }
}
